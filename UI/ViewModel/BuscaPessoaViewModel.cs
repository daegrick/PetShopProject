using BLL.Controle;
using DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Services;

namespace UI.ViewModel
{
    public class BuscaPessoaViewModel : ViewModelBase
    {
        #region Setters
        private string? pesquisaNome;
        private ObservableCollection<Pessoa> pessoas;
        public PessoaViewModel? PessoaViewModel { get; set; }
        private readonly WindowService WindowService; 
        private Pessoa? selectedPessoa;

        public Pessoa? SelectedPessoa
        {
            get => selectedPessoa;
            set => OnPropertyChanged(ref selectedPessoa, value, nameof(SelectedPessoa));
        }

        public ObservableCollection<Pessoa> Pessoas
        {
            get => pessoas;
            set => OnPropertyChanged(ref pessoas, value, nameof(Pessoas));
        }
        public string PesquisaNome
        {
            get => pesquisaNome ??= string.Empty;
            set => OnPropertyChanged(ref pesquisaNome, value, nameof(PesquisaNome));
        }
        #endregion
        #region Commands
        private ICommand buscaPessoaCommand;
        private ICommand editaPessoaCommand;

        public ICommand EditaPessoaCommand => editaPessoaCommand ?? (editaPessoaCommand = new RelayCommand(EditaPessoa, canExecute => SelectedPessoa != null));
        public ICommand BuscaPessoaCommand => buscaPessoaCommand ?? (buscaPessoaCommand = new RelayCommand(BuscaPessoa));
        #endregion

        #region Methods
        public void EditaPessoa(object o)
        {
            PessoaViewModel ??= new PessoaViewModel();
            PessoaViewModel.LoadPessoa(SelectedPessoa);
            WindowService.AbrirTelaPessoa(PessoaViewModel);
        }
        public void BuscaPessoa(object o = null)
        {
            Pessoas.Clear();
            foreach (var pessoa in PessoaBLL.ListaPessoas(PesquisaNome))
            {
                Pessoas.Add(pessoa);
            }
        }
        #endregion
        #region Constructor
        public BuscaPessoaViewModel() {

            pesquisaNome = string.Empty;
            WindowService = new();
            Pessoas = new();
            BuscaPessoa();
        }
        #endregion
    }
}