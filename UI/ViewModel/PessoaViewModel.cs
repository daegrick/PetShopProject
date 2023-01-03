using BLL.Controle;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Extras;

namespace UI.ViewModel
{
    public class PessoaViewModel : ViewModelBase, IDataErrorInfo
    {
        private int codigo;
        private string nome;
        private string sexo;
        private DateTime nascimento;
        private Guid ide;
        private bool ativo;
        private bool isMasculino;
        private bool isFeminino;
        private bool isOutro;
        private List<string> errors = new();



        #region Actions
        private Action CloseWindow;
        #endregion

        #region Setters


        public int Codigo
        {
            get => codigo;
            set => OnPropertyChanged(ref codigo, value, nameof(Codigo));
        }
        public string Nome
        {
            get => nome;
            set => OnPropertyChanged(ref nome, value, nameof(Nome));
        }
        public string Sexo
        {
            get => sexo;
            set => OnPropertyChanged(ref sexo, value, nameof(Sexo));
        }
        public DateTime Nascimento
        {
            get => nascimento;
            set => OnPropertyChanged(ref nascimento, value, nameof(Nascimento));
        }
        public Guid Ide
        {
            get => ide;
            set => OnPropertyChanged(ref ide, value, nameof(Ide));
        }
        public bool Ativo
        {
            get => ativo;
            set => OnPropertyChanged(ref ativo, value, nameof(Ativo));
        }
        public bool IsMasculino
        {
            get => isMasculino;
            set {
                if(isMasculino != value)
                    isMasculino = value;
                if (isMasculino)
                    Sexo = "M";
                RaisePropertyChange(nameof(IsMasculino));
            }
        }
        public bool IsFeminino
        {
            get => isFeminino;
            set {
                if (isFeminino != value)
                    isFeminino= value;
                if (isFeminino)
                    Sexo = "F";
                RaisePropertyChange(nameof(IsFeminino));
            }
        }
        public bool IsOutro
        {
            get => isOutro;
            set {
                if (isOutro!= value)
                    isOutro= value;
                if (isOutro)
                    Sexo = "O";
                RaisePropertyChange(nameof(IsOutro));
            }
        }
        #endregion

        #region Commands
        private ICommand inserePessoaCommand;
        private ICommand novaPessoaCommand;
        public ICommand InserePessoaCommand => inserePessoaCommand ?? (new RelayCommand(InserePessoa, canExecute => CanInserirPessoa));
        

        public ICommand NovaPessoaCommand => novaPessoaCommand ?? (new RelayCommand(NovaPessoa));
        #endregion

        #region Methods
        public void NovaPessoa(object o)
        {
            Codigo = 0;
            Nome =string.Empty;
            IsMasculino = true;
            Nascimento = DateTime.Now;
            Ide = Guid.Empty;
            Ativo = true;
        }
        public void InserePessoa(object o)
        {
            Pessoa pessoa = new Pessoa()
            {
                Codigo = Codigo,
                Nome = Nome,
                Nascimento = Nascimento,
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
                    isOutro = true;
                    break;
            }
            Nascimento = pessoa.Nascimento;
            Ide = pessoa.Ide;
        }

        #endregion

        #region Validation
        private bool CanInserirPessoa
        {
            get
            {
                errors.Clear();
                errors.UniqueIfNotEmpty(this[nameof(Sexo)]);
                errors.UniqueIfNotEmpty(this[nameof(Nascimento)]);
                errors.UniqueIfNotEmpty(this[nameof(Nome)]);
                return errors.Count == 0;
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
                    case nameof(Nascimento):
                        if (Nascimento < new DateTime(1800, 1, 10) || Nascimento > DateTime.Now.Subtract(TimeSpan.FromDays(365)))
                            return "Entre com uma data válida!";
                        break;
                }
                return string.Empty;
            }
        }
        public string Error => throw new NotImplementedException();
        #endregion

        #region constructor
        public PessoaViewModel()
        {
        }
        #endregion
    }
}