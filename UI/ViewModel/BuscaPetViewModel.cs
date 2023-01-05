using BLL.Controle;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Services;

namespace UI.ViewModel
{
    public class BuscaPetViewModel : ViewModelBase
    {
        #region Setters
        private ObservableCollection<Pet> pets;
        private string pesquisaNome;
        private Pet selectedPet;
        private Action<Pet> selectedAction;
        private Dictionary<Guid, string> racaDictionary;
        private bool? isAdotado;
        private readonly WindowService WindowService;
        public PetViewModel PetViewModel { get; private set; }
        public bool CanInserirPet => (SelectedPet != null && selectedAction != null);

        public bool? IsAdotado
        {
            get => isAdotado;
            set => OnPropertyChanged(ref isAdotado, value, nameof(IsAdotado));
        }


        public Dictionary<Guid, string> RacaDictionary
        {
            get => racaDictionary;
            set => OnPropertyChanged(ref racaDictionary, value, nameof(RacaDictionary));
        }


        public Pet SelectedPet
        {
            get => selectedPet;
            set => OnPropertyChanged(ref selectedPet, value, nameof(SelectedPet));
        }

        public ObservableCollection<Pet> Pets
        {
            get => pets;
            set => OnPropertyChanged(ref pets, value, nameof(Pets));
        }

        public string PesquisaNome
        {
            get => pesquisaNome;
            set => OnPropertyChanged(ref pesquisaNome, value, nameof(PesquisaNome));
        }

        #endregion

        public void EscolhePet(object o)
        {
            selectedAction(SelectedPet);
        }
        public void BuscaPet(object o)
        {
            Pets.Clear();
            foreach (var pet in PetBLL.Buscar(PesquisaNome, IsAdotado))
            {
                Pets.Add(pet);
            }
        }

        internal void Load(Action<Pet> inserePetDaBusca)
        {
            selectedAction = inserePetDaBusca;
        }
        #region Commands
        private ICommand inserePetCommand;
        private ICommand buscaPetCommand;
        private ICommand editaPetCommand;
        public ICommand BuscaPetCommand => buscaPetCommand ?? new RelayCommand(BuscaPet);
        public ICommand InserePetCommand => inserePetCommand ?? new RelayCommand(EscolhePet, canExecute => CanInserirPet);
        public ICommand EditaPetCommand => editaPetCommand ?? (editaPetCommand = new RelayCommand(EditaPet, canExecute => SelectedPet != null));
        public void EditaPet(object o)
        {
            PetViewModel ??= new PetViewModel();
            PetViewModel.LoadPet(SelectedPet);
            WindowService.AbrirTelaPet(PetViewModel);
        }

        #endregion

        public BuscaPetViewModel()
        {
            RacaDictionary = RacaBLL.BuscaDictionary();
            Pets = new ObservableCollection<Pet>();
            pesquisaNome = string.Empty;
            WindowService = new WindowService();
        }
        public BuscaPetViewModel(Action<Pet> inserePetAction) : this()
        {
            selectedAction = inserePetAction;
        }
    }
}