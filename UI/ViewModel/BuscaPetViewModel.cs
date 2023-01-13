using BLL.Controle;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Services;

namespace UI.ViewModel
{
    public class BuscaPetViewModel : ViewModelBase, IDisposable
    {
        #region Setters
        private ObservableCollection<Pet> _pets;
        private string _pesquisaNome;
        private Pet _selectedPet;
        private Action<Pet> _selectedAction;
        private Dictionary<Guid, string> _racaDictionary;
        private bool? _isAdotado;
        private readonly WindowService _windowService;

        public PetViewModel PetViewModel { get; private set; }
        public bool CanInserirPet => (SelectedPet != null && _selectedAction != null);
        public bool? IsAdotado
        {
            get => _isAdotado;
            set => OnPropertyChanged(ref _isAdotado, value, nameof(IsAdotado));
        }
        public Dictionary<Guid, string> RacaDictionary
        {
            get => _racaDictionary;
            set => OnPropertyChanged(ref _racaDictionary, value, nameof(RacaDictionary));
        }
        public Pet SelectedPet
        {
            get => _selectedPet;
            set => OnPropertyChanged(ref _selectedPet, value, nameof(SelectedPet));
        }
        public ObservableCollection<Pet> Pets
        {
            get => _pets;
            set => OnPropertyChanged(ref _pets, value, nameof(Pets));
        }
        public string PesquisaNome
        {
            get => _pesquisaNome;
            set => OnPropertyChanged(ref _pesquisaNome, value, nameof(PesquisaNome));
        }

        #endregion

        #region Commands
        private ICommand _inserePetCommand;
        private ICommand _buscaPetCommand;
        private ICommand _editaPetCommand;

        public ICommand BuscaPetCommand => _buscaPetCommand ??= new RelayCommand(BuscaPet);
        public ICommand InserePetCommand => _inserePetCommand ??= new RelayCommand(EscolhePet, canExecute => CanInserirPet);
        public ICommand EditaPetCommand => _editaPetCommand ??= new RelayCommand(EditaPet, canExecute => SelectedPet != null);



        #endregion

        #region Methods
        public void EditaPet(object o)
        {
            PetViewModel ??= new PetViewModel();
            PetViewModel.LoadPet(SelectedPet);
            _windowService.AbrirTelaPet(PetViewModel);
        }
        public void EscolhePet(object o)
        {
            _selectedAction(SelectedPet);
        }
        public void BuscaPet(object o)
        {
            Pets.Clear();
            foreach (var pet in PetBLL.Buscar(PesquisaNome, IsAdotado))
            {
                Pets.Add(pet);
            }
        }

        public void Dispose()
        {
            Pets.Clear();
            SelectedPet = null;
            PesquisaNome = string.Empty;
        }

        #endregion

        #region Constructor
        public BuscaPetViewModel()
        {
            RacaDictionary = RacaBLL.BuscaDictionary();
            Pets = new ObservableCollection<Pet>();
            _pesquisaNome = string.Empty;
            _windowService = new WindowService();
        }
        public BuscaPetViewModel(Action<Pet> inserePetAction) : this()
        {
            _selectedAction = inserePetAction;
        }
        #endregion

    }
}