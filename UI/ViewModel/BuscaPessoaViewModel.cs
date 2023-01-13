using BLL.Controle;
using DTO;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Services;

namespace UI.ViewModel
{
    public class BuscaPessoaViewModel : ViewModelBase, IDisposable
    {
        #region Setters
        private string? _pesquisaNome;
        private ObservableCollection<Pessoa> _pessoas;
        private PessoaViewModel? _pessoaViewModel;
        private readonly WindowService _windowService; 
        private Pessoa? _selectedPessoa;

        public Pessoa? SelectedPessoa
        {
            get => _selectedPessoa;
            set => OnPropertyChanged(ref _selectedPessoa, value, nameof(SelectedPessoa));
        }
        public ObservableCollection<Pessoa> Pessoas
        {
            get => _pessoas;
            set => OnPropertyChanged(ref _pessoas, value, nameof(Pessoas));
        }
        public string PesquisaNome
        {
            get => _pesquisaNome ??= string.Empty;
            set => OnPropertyChanged(ref _pesquisaNome, value, nameof(PesquisaNome));
        }

        #endregion

        #region Commands
        private ICommand _buscaPessoaCommand;
        private ICommand _editaPessoaCommand;

        public ICommand EditaPessoaCommand => _editaPessoaCommand ??= new RelayCommand(EditaPessoa, canExecute => SelectedPessoa != null);
        public ICommand BuscaPessoaCommand => _buscaPessoaCommand ??= new RelayCommand(BuscaPessoa);
        #endregion

        #region Methods
        public void EditaPessoa(object o)
        {
            _pessoaViewModel ??= new PessoaViewModel();
            _pessoaViewModel.LoadPessoa(SelectedPessoa);
            _windowService.AbrirTelaPessoa(_pessoaViewModel);
        }
        public void BuscaPessoa(object o = null)
        {
            Pessoas.Clear();
            foreach (var pessoa in PessoaBLL.ListaPessoas(PesquisaNome))
            {
                Pessoas.Add(pessoa);
            }
        }

        public void Dispose()
        {
            Pessoas.Clear();
            SelectedPessoa = null;
        }
        #endregion

        #region Constructor
        public BuscaPessoaViewModel() {

            _pesquisaNome = string.Empty;
            _windowService = new();
            Pessoas = new();
            BuscaPessoa();
        }
        #endregion
    }
}