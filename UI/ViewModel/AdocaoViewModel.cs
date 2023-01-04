using BLL.Controle;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace UI.ViewModel
{
    public class AdocaoViewModel : ViewModelBase
    {
        #region Setters
        private PetSearchUI petSearchUI;
        private ObservableCollection<Pessoa> pessoas;
        private ObservableCollection<Pet> pets;
        private Pessoa selectedPessoa;
        private Pet selectedPet;
        private bool isDataGridPessoaLocked;
        private BuscaPetViewModel buscaPetViewModel;
        private Dictionary<Guid,string> racaDictionary;

        public Dictionary<Guid,string> RacaDictionary
        {
            get => racaDictionary;
            set => OnPropertyChanged(ref racaDictionary, value, nameof(RacaDictionary));
        }

        public BuscaPetViewModel BuscaPetViewModel
        {
            get => buscaPetViewModel;
            set => OnPropertyChanged(ref buscaPetViewModel, value, nameof(BuscaPetViewModel));
        }

        public bool IsDataGridPessoaLocked
        {
            get => isDataGridPessoaLocked;
            set => OnPropertyChanged(ref isDataGridPessoaLocked, value, nameof(IsDataGridPessoaLocked));
        }

        public Pet SelectedPet
        {
            get => selectedPet;
            set => OnPropertyChanged(ref selectedPet, value, nameof(SelectedPet));
        }

        public Pessoa SelectedPessoa
        {
            get => selectedPessoa;
            set => UpdateLista(value);
        }


        public ObservableCollection<Pet> Pets
        {
            get => pets;
            set => OnPropertyChanged(ref pets, value, nameof(Pets));
        }

        public ObservableCollection<Pessoa> Pessoas
        {
            get => pessoas;
            set => OnPropertyChanged(ref pessoas, value, nameof(Pessoas));
        }

        #endregion

        #region Commands
        private ICommand adotarCommand;
        private ICommand cancelarAdocaoCommand; 
        private ICommand adicionarPetCommand;

        public ICommand AdicionarPetCommand => adicionarPetCommand ?? (adicionarPetCommand = new RelayCommand(AdicionaPet, canExecute => SelectedPessoa != null));
        public ICommand CancelarAdocaoCommand => cancelarAdocaoCommand ?? (cancelarAdocaoCommand = new RelayCommand(CancelaAdocao, canExecute => SelectedPet != null));
        public ICommand AdotarCommand => adotarCommand ?? (adotarCommand = new RelayCommand(Adotar, canExecute => true));

        #endregion

        #region Methods
        public void AdicionaPet(object o)
        {
            petSearchUI.ShowDialog();
            IsDataGridPessoaLocked = false;
        }

        public void InserePet(Pet pet)
        {
            if (!pet.IsAdotado)
            {
                Pets.Add(pet);
                return;
            }
            MessageBox.Show("Este pet já está adotado!");
        }

        public void CancelaAdocao(object o)
        {
            Pets.Remove(SelectedPet);
        }

        public void Adotar(object o)
        {
            MessageBox.Show(AdocaoBLL.Adotar(SelectedPessoa, Pets));
        }

        public void UpdateLista(Pessoa value)
        {
            Pets.Clear();
            OnPropertyChanged(ref selectedPessoa, value, nameof(SelectedPessoa));
            foreach (var pet in PetBLL.BuscarPetsAdotados(SelectedPessoa))
            {
                Pets.Add(pet);
            }
            
        }
        #endregion

        public AdocaoViewModel()
        {
            petSearchUI = new PetSearchUI();
            BuscaPetViewModel = new();
            BuscaPetViewModel.Load(InserePet);
            petSearchUI.DataContext = this;
            Pessoas = new ObservableCollection<Pessoa>();
            Pets = new ObservableCollection<Pet>();
            foreach (var pessoa in PessoaBLL.ListaPessoas(string.Empty))
            {
                Pessoas.Add(pessoa);
            }
            RacaDictionary = RacaBLL.BuscaDictionary();
        }
    }
}