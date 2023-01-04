using System;
using System.Windows.Input;
using UI.Services;

namespace UI.ViewModel
{
    /*
     * TODO: Não recomendado incluir membros de UI nos viewModels; verificar outra forma de navegar por essas telas
     */
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
        private ICommand abrirTelaPessoa;
        private ICommand abrirTelaPet;
        private ICommand abrirTelaRaca;
        private ICommand abrirTelaBuscaPet;
        private ICommand abrirTelaBuscaPessoa;
        private ICommand abrirTelaAdocao;

        public ICommand? AbrirTelaAdocao => abrirTelaAdocao ??= new RelayCommand(AbreTelaAdocao);
        public ICommand? AbrirTelaBuscaPessoa => abrirTelaBuscaPessoa ??= new RelayCommand(AbreTelaBuscaPessoa);
        public ICommand? AbrirTelaPessoa => abrirTelaPessoa ??= new RelayCommand(AbreTelaPessoa);
        public ICommand? AbrirTelaPet => abrirTelaPet ??= new RelayCommand(AbreTelaPet);
        public ICommand? AbrirTelaRaca => abrirTelaRaca ??= new RelayCommand(AbreTelaRaca);
        public ICommand? AbrirTelaBuscaPet => abrirTelaBuscaPet ??= new RelayCommand(AbreTelaBuscaPet);
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

        public MainViewModel()
        {
            WindowService = new WindowService();
        }
    }
}
