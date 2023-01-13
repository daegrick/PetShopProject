using BLL.Controle;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UI.Extras;

namespace UI.ViewModel
{

    public class PetViewModel : ViewModelBase, IDataErrorInfo, IDisposable
    {
        #region Setters
        private int _codigo;
        private string? _nome;
        private string? _sexo;
        private DateTime _dataNascimento;
        private int _quantidadeVacinas;
        private Guid _ide;
        private Guid _racaIde;
        private bool _ativo;
        private bool _isMacho;
        private bool _isFemea;
        private readonly List<string> _errors;
        private RacaViewModel _racaViewModel;
        private Raca _raca;

        public Raca Raca
        {
            get => _raca;
            set => OnPropertyChanged(ref _raca, value, nameof(Raca));
        }
        public RacaViewModel RacaViewModel
        {
            get => _racaViewModel;
            set => OnPropertyChanged(ref _racaViewModel, value);
        }
        public bool IsMacho
        {
            get => _isMacho;
            set {
                if (_isMacho != value)
                    _isMacho = value;
                if (_isMacho)
                    Sexo = "M";
                RaisePropertyChange(nameof(IsMacho));
            }
        }
        public bool IsFemea
        {
            get => _isFemea;
            set {
                if (_isFemea != value)
                    _isFemea = value;
                if (_isFemea)
                    Sexo = "F";
                RaisePropertyChange(nameof(IsFemea));
            }
        }
        public int Codigo
        {
            get => _codigo;
            set => OnPropertyChanged(ref _codigo, value, nameof(Codigo));
        }
        public string Nome
        {
            get => _nome ?? string.Empty;
            set => OnPropertyChanged(ref _nome, value, nameof(Nome));
        }
        public string Sexo
        {
            get => _sexo ?? string.Empty;
            set => OnPropertyChanged(ref _sexo, value, nameof(Sexo));
        }
        public DateTime DataNascimento
        {
            get => _dataNascimento;
            set => OnPropertyChanged(ref _dataNascimento, value, nameof(DataNascimento));
        }
        public int QuantidadeVacinas
        {
            get => _quantidadeVacinas;
            set => OnPropertyChanged(ref _quantidadeVacinas, value, nameof(QuantidadeVacinas));
        }
        public Guid RacaIde
        {
            get => _racaIde;
            set => OnPropertyChanged(ref _racaIde, value, nameof(RacaIde));
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
        #endregion

        #region Commands
        private ICommand inserePetCommand;
        private ICommand novoPetCommand;

        public ICommand NovoPetCommand => novoPetCommand ??= new RelayCommand(NovoPet, canExecute=>true);

        public ICommand InserePetCommand => inserePetCommand ?? (inserePetCommand = new RelayCommand(InserePet, canExecute => _canInserirPet));


        #endregion

        #region Validation
        public bool _canInserirPet
        {
            get {
                _errors.Clear();
                _errors.UniqueIfNotEmpty(this[nameof(Nome)]);
                _errors.UniqueIfNotEmpty(this[nameof(DataNascimento)]);
                _errors.UniqueIfNotEmpty(this[nameof(QuantidadeVacinas)]);
                _errors.UniqueIfNotEmpty(this[nameof(Raca)]);
                return _errors.Count == 0;
            }
        }
        public string Error => throw new NotImplementedException();
        public string this[string columnName]

        {
            get {
                switch (columnName)
                {
                    case nameof(Nome):
                        if (string.IsNullOrEmpty(Nome))
                            return "Entre com um nome!";
                        break;
                    case nameof(DataNascimento):
                        if (DataNascimento < new DateTime(1800, 1, 10) || DataNascimento > DateTime.Now.Subtract(TimeSpan.FromDays(365)))
                            return "Entre com uma data válida!";
                        break;
                    case nameof(QuantidadeVacinas):
                        if (QuantidadeVacinas < 0)
                            return "Quantidade invalida de vacinas!";
                        break;
                    case nameof(Raca):
                        if (Raca is null || Raca.Ide == Guid.Empty)
                            return "Escolha a raça do pet!";
                        break;
                }
                return string.Empty;
            }
        }
        #endregion

        #region Methods
        private void EscolheSexo(string sexo)
        {
            if (sexo == "M")
            {
                IsMacho = true;
            }
            else
            {
                IsFemea = true;
            }
        }
        private void EscolheRaca(Guid racaIde)
        {
            foreach (var raca in RacaViewModel.Racas)
            {
                if (raca.Ide != racaIde)
                    continue;
                Raca = raca;
                break;
            }
        }
        public void NovoPet(object o)
        {
            LoadPet(new Pet() { Sexo = "M" });
        }
        public void InserePet(object o)
        {
            Pet pet = new()
            {
                Codigo = Codigo,
                Nome = Nome,
                Sexo = Sexo,
                DataNascimento = DataNascimento,
                QuantidadeVacinas = QuantidadeVacinas,
                Ide = Ide,
                RacaIde = Raca.Ide,
                CodigoRaca = Raca.Codigo,
                Ativo = Ativo
            };
            MessageBox.Show(PetBLL.Inserir(pet));
        }
        public void Dispose()
        {
            LoadPet(new Pet());
        }
        #endregion

        #region Constructor
        public PetViewModel()
        {
            _errors = new List<string>();
            RacaViewModel = new RacaViewModel();
            IsMacho = true;
        }
        public void LoadPet(Pet pet)
        {
            Codigo = pet.Codigo;
            Nome = pet.Nome;
            EscolheSexo(pet.Sexo);
            DataNascimento = pet.DataNascimento;
            QuantidadeVacinas = pet.QuantidadeVacinas;
            Ide = pet.Ide;
            EscolheRaca(pet.RacaIde);
        }

        
        #endregion

    }
}