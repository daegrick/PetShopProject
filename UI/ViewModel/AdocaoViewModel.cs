using BLL.Controle;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using UI.Services;

namespace UI.ViewModel
{
    public class AdocaoViewModel : ViewModelBase
    {
        #region Setters
        private ObservableCollection<Pessoa> _pessoas;
        private ObservableCollection<Pet> _pets;
        private Pessoa _selectedPessoa;
        private Pet _selectedPet;
        private bool _isDataGridPessoaLocked;
        private Dictionary<Guid,string> _racaDictionary;
        private readonly WindowService _windowService;
        private BuscaPetViewModel _buscaPetViewModel;

        public Dictionary<Guid,string> RacaDictionary
        {
            get => _racaDictionary;
            set => OnPropertyChanged(ref _racaDictionary, value, nameof(RacaDictionary));
        }
        public bool IsDataGridPessoaLocked
        {
            get => _isDataGridPessoaLocked;
            set => OnPropertyChanged(ref _isDataGridPessoaLocked, value, nameof(IsDataGridPessoaLocked));
        }
        public Pet SelectedPet
        {
            get => _selectedPet;
            set => OnPropertyChanged(ref _selectedPet, value, nameof(SelectedPet));
        }
        public Pessoa SelectedPessoa
        {
            get => _selectedPessoa;
            set => UpdateLista(value);
        }
        public ObservableCollection<Pet> Pets
        {
            get => _pets;
            set => OnPropertyChanged(ref _pets, value, nameof(Pets));
        }
        public ObservableCollection<Pessoa> Pessoas
        {
            get => _pessoas;
            set => OnPropertyChanged(ref _pessoas, value, nameof(Pessoas));
        }

        #endregion

        #region Commands
        private ICommand _adotarCommand;
        private ICommand _cancelarAdocaoCommand; 
        private ICommand _adicionarPetCommand;

        public ICommand AdicionarPetCommand => _adicionarPetCommand ??= new RelayCommand(AdicionaPet, canExecute => SelectedPessoa != null);
        public ICommand CancelarAdocaoCommand => _cancelarAdocaoCommand ??= new RelayCommand(CancelaAdocao, canExecute => SelectedPet != null);
        public ICommand AdotarCommand => _adotarCommand ??= new RelayCommand(Adotar, canExecute => true);

        #endregion

        #region Methods
        public void AdicionaPet(object o)
        {
            _buscaPetViewModel ??= new BuscaPetViewModel(InserePet);
            _windowService.AbrirTelaBuscaPet(_buscaPetViewModel);
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
            OnPropertyChanged(ref _selectedPessoa, value, nameof(SelectedPessoa));
            foreach (var pet in PetBLL.BuscarPetsAdotados(SelectedPessoa))
            {
                Pets.Add(pet);
            }
            
        }
        #endregion

        #region Constructor
        public AdocaoViewModel()
        {
            Pessoas = new ObservableCollection<Pessoa>();
            Pets = new ObservableCollection<Pet>();
            _windowService = new WindowService();
            foreach (var pessoa in PessoaBLL.ListaPessoas(string.Empty))
            {
                Pessoas.Add(pessoa);
            }
            RacaDictionary = RacaBLL.BuscaDictionary();
        }
        #endregion
    }
}