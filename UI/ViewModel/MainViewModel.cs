using System.Windows.Input;
using UI.Services;

namespace UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Setter
        public AdocaoViewModel? AdocaoViewModel { get; set; }
        public PetViewModel? PetViewModel { get; set; }
        public PessoaViewModel? PessoaViewModel { get; set; }
        public RacaViewModel? RacaViewModel { get; set; }
        public BuscaPessoaViewModel? BuscaPessoaViewModel { get; set; }
        public BuscaPetViewModel? BuscaPetViewModel { get; set; }
        public readonly WindowService WindowService;
        #endregion

        #region Commands
        private ICommand _abrirTelaPessoa;
        private ICommand _abrirTelaPet;
        private ICommand _abrirTelaRaca;
        private ICommand _abrirTelaBuscaPet;
        private ICommand _abrirTelaBuscaPessoa;
        private ICommand _abrirTelaAdocao;

        public ICommand? AbrirTelaAdocao => _abrirTelaAdocao ??= new RelayCommand(AbreTelaAdocao);
        public ICommand? AbrirTelaBuscaPessoa => _abrirTelaBuscaPessoa ??= new RelayCommand(AbreTelaBuscaPessoa);
        public ICommand? AbrirTelaPessoa => _abrirTelaPessoa ??= new RelayCommand(AbreTelaPessoa);
        public ICommand? AbrirTelaPet => _abrirTelaPet ??= new RelayCommand(AbreTelaPet);
        public ICommand? AbrirTelaRaca => _abrirTelaRaca ??= new RelayCommand(AbreTelaRaca);
        public ICommand? AbrirTelaBuscaPet => _abrirTelaBuscaPet ??= new RelayCommand(AbreTelaBuscaPet);
        #endregion

        #region Methods
        public void AbreTelaRaca(object o)
        {
            WindowService.AbrirTelaRaca();
        }
        public void AbreTelaBuscaPet(object o)
        {
            WindowService.AbrirTelaBuscaPet(BuscaPetViewModel ??= new BuscaPetViewModel());
        }
        public void AbreTelaAdocao(object o)
        {
            WindowService.AbrirTelaAdocao(AdocaoViewModel ??= new AdocaoViewModel());
        }

        public void AbreTelaBuscaPessoa(object o)
        {
            WindowService.AbrirTelaBuscaPessoa (BuscaPessoaViewModel ??= new BuscaPessoaViewModel());
        }
        public void AbreTelaPessoa(object o)
        {
            WindowService.AbrirTelaPessoa(PessoaViewModel ??= new PessoaViewModel());
        }

        public void AbreTelaPet(object o)
        {
            WindowService.AbrirTelaPet(PetViewModel??= new PetViewModel());
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            WindowService = new WindowService();
        }
        #endregion
    }
}
