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

    public class PetViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Setters
        private int codigo;
        private string? nome;
        private string? sexo;
        private DateTime dataNascimento;
        private int quantidadeVacinas;
        private Guid ide;
        private Guid racaIde;
        private bool ativo;
        private bool isMacho;
        private bool isFemea;
        private List<string> errors;
        private RacaViewModel racaViewModel;
        private Raca raca;

        public Raca Raca
        {
            get => raca;
            set => OnPropertyChanged(ref raca, value, nameof(Raca));
        }


        public RacaViewModel RacaViewModel
        {
            get => racaViewModel;
            set => OnPropertyChanged(ref racaViewModel, value);
        }

        public bool IsMacho
        {
            get => isMacho;
            set {
                if (isMacho != value)
                    isMacho = value;
                if (isMacho)
                    Sexo = "M";
                RaisePropertyChange(nameof(IsMacho));
            }
        }
        public bool IsFemea
        {
            get => isFemea;
            set {
                if (isFemea != value)
                    isFemea = value;
                if (isFemea)
                    Sexo = "F";
                RaisePropertyChange(nameof(IsFemea));
            }
        }
        public int Codigo
        {
            get => codigo;
            set => OnPropertyChanged(ref codigo, value, nameof(Codigo));
        }
        public string Nome
        {
            get => nome ?? string.Empty;
            set => OnPropertyChanged(ref nome, value, nameof(Nome));
        }
        public string Sexo
        {
            get => sexo ?? string.Empty;
            set => OnPropertyChanged(ref sexo, value, nameof(Sexo));
        }
        public DateTime DataNascimento
        {
            get => dataNascimento;
            set => OnPropertyChanged(ref dataNascimento, value, nameof(DataNascimento));
        }
        public int QuantidadeVacinas
        {
            get => quantidadeVacinas;
            set => OnPropertyChanged(ref quantidadeVacinas, value, nameof(QuantidadeVacinas));
        }
        public Guid RacaIde
        {
            get => racaIde;
            set => OnPropertyChanged(ref racaIde, value, nameof(RacaIde));
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
        #endregion

        #region Commands
        private ICommand inserePetCommand;

        public ICommand InserePetCommand => inserePetCommand ?? (inserePetCommand = new RelayCommand(InserePet, canExecute => canInserirPet));


        #endregion

        #region Validation
        public bool canInserirPet
        {
            get {
                errors.Clear();
                errors.UniqueIfNotEmpty(this[nameof(Nome)]);
                errors.UniqueIfNotEmpty(this[nameof(DataNascimento)]);
                errors.UniqueIfNotEmpty(this[nameof(QuantidadeVacinas)]);
                errors.UniqueIfNotEmpty(this[nameof(Raca)]);
                return errors.Count == 0;
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
        #endregion

        public PetViewModel()
        {
            errors = new List<string>();
            RacaViewModel = new RacaViewModel();
            isMacho = true;
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
    }
}