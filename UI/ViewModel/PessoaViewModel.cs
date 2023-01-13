using BLL.Controle;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using UI.Extras;

namespace UI.ViewModel
{
    public class PessoaViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Setters
        private int _codigo;
        private string _nome;
        private string _sexo;
        private DateTime _dataNascimento;
        private Guid _ide;
        private bool _ativo;
        private bool _isMasculino;
        private bool _isFeminino;
        private bool _isOutro;
        private readonly List<string> _errors;
     
        public int Codigo
        {
            get => _codigo;
            set => OnPropertyChanged(ref _codigo, value, nameof(Codigo));
        }
        public string Nome
        {
            get => _nome;
            set => OnPropertyChanged(ref _nome, value, nameof(Nome));
        }
        public string Sexo
        {
            get => _sexo;
            set => OnPropertyChanged(ref _sexo, value, nameof(Sexo));
        }
        public DateTime DataNascimento
        {
            get => _dataNascimento;
            set => OnPropertyChanged(ref _dataNascimento, value, nameof(DataNascimento));
        }
        public Guid Ide
        {
            get => _ide;
            set => OnPropertyChanged(ref _ide, value, nameof(Ide));
        }
        public bool Ativo
        {
            get => _ativo;
            set => OnPropertyChanged(ref _ativo, value, nameof(Ativo));
        }
        public bool IsMasculino
        {
            get => _isMasculino;
            set {
                if(_isMasculino != value)
                    _isMasculino = value;
                if (_isMasculino)
                    Sexo = "M";
                RaisePropertyChange(nameof(IsMasculino));
            }
        }
        public bool IsFeminino
        {
            get => _isFeminino;
            set {
                if (_isFeminino != value)
                    _isFeminino= value;
                if (_isFeminino)
                    Sexo = "F";
                RaisePropertyChange(nameof(IsFeminino));
            }
        }
        public bool IsOutro
        {
            get => _isOutro;
            set {
                if (_isOutro!= value)
                    _isOutro= value;
                if (_isOutro)
                    Sexo = "O";
                RaisePropertyChange(nameof(IsOutro));
            }
        }
        #endregion

        #region Commands
        private ICommand _inserePessoaCommand;
        private ICommand _novaPessoaCommand;

        public ICommand InserePessoaCommand => _inserePessoaCommand ??= new RelayCommand(InserePessoa, canExecute => CanInserirPessoa);
        public ICommand NovaPessoaCommand => _novaPessoaCommand ??= new RelayCommand(NovaPessoa);
        #endregion

        #region Methods
        public void NovaPessoa(object o)
        {
            LoadPessoa(new Pessoa());
        }
        public void InserePessoa(object o)
        {
            Pessoa pessoa = new Pessoa()
            {
                Codigo = Codigo,
                Nome = Nome,
                DataNascimento = DataNascimento,
                Sexo = Sexo,
                Ide = Ide,
                Ativo = Ativo
            };
            MessageBox.Show(PessoaBLL.Incluir(pessoa));
        }
        public void LoadPessoa(Pessoa pessoa)
        {
            Codigo = pessoa.Codigo;
            Nome = pessoa.Nome;
            switch (pessoa.Sexo)
            {
                case "M":
                    IsMasculino = true;
                    break;
                case "F":
                    IsFeminino = true;
                    break;
                case "O":
                    _isOutro = true;
                    break;
            }
            DataNascimento = pessoa.DataNascimento;
            Ide = pessoa.Ide;
        }

        #endregion

        #region Validation
        private bool CanInserirPessoa
        {
            get
            {
                _errors.Clear();
                _errors.UniqueIfNotEmpty(this[nameof(Sexo)]);
                _errors.UniqueIfNotEmpty(this[nameof(DataNascimento)]);
                _errors.UniqueIfNotEmpty(this[nameof(Nome)]);
                return _errors.Count == 0;
            }
        }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Nome):
                        if (string.IsNullOrEmpty(Nome))
                            return "Entre com um nome!";
                        // TODO inserir validação Regex (?)
                        break;
                    case nameof(DataNascimento):
                        if (DataNascimento < new DateTime(1800, 1, 10) || DataNascimento > DateTime.Now.Subtract(TimeSpan.FromDays(365)))
                            return "Entre com uma data válida!";
                        break;
                }
                return string.Empty;
            }
        }
        public string Error => throw new NotImplementedException();
        #endregion

        #region Constructor
        public PessoaViewModel()
        {
            _errors = new();
            IsMasculino = true;
        }
        #endregion
    }
}