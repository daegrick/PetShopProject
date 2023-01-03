using System;
using System.Windows.Input;

namespace UI.ViewModel
{
    /*
     * TODO: Não recomendado incluir membros de UI nos viewModels; verificar outra forma de navegar por essas telas
     */
    public class MainViewModel : ViewModelBase
    {
        #region Setter
        private PessoaUI telaPessoa;
        private PetUI telaPet;
        private PetSearchUI telaBuscaPet;
        private PessoaSearchUI telaBuscaPessoa;
        private PessoaViewModel pessoaViewModel;
        private PetViewModel petViewModel;
        private BuscaPetViewModel buscaPetViewModel;
        private BuscaPessoaViewModel buscaPessoaViewModel; 
        private RacaViewModel racaViewModel;
        private RacaUI telaRaca; 
        private AdocaoViewModel adocaoViewModel;
        private AdocaoUI telaAdocao;

        public AdocaoUI TelaAdocao
        {
            get => telaAdocao;
            set => OnPropertyChanged(ref telaAdocao, value, nameof(TelaAdocao));
        }


        public AdocaoViewModel AdocaoViewModel
        {
            get => adocaoViewModel;
            set => OnPropertyChanged(ref adocaoViewModel, value, nameof(AdocaoViewModel));
        }


        public RacaUI TelaRaca
        {
            get => telaRaca;
            set => OnPropertyChanged(ref telaRaca, value, nameof(TelaRaca));
        }
        public RacaViewModel RacaViewModel
        {
            get => racaViewModel;
            set => OnPropertyChanged(ref racaViewModel, value, nameof(RacaViewModel));
        }
        public PessoaSearchUI TelaBuscaPessoa
        {
            get => telaBuscaPessoa;
            set => OnPropertyChanged(ref telaBuscaPessoa, value, nameof(TelaBuscaPessoa));
        }
        public BuscaPessoaViewModel BuscaPessoaViewModel
        {
            get => buscaPessoaViewModel;
            set => OnPropertyChanged(ref buscaPessoaViewModel, value, nameof(BuscaPessoaViewModel));
        }
        public BuscaPetViewModel BuscaPetViewModel
        {
            get => buscaPetViewModel;
            set => OnPropertyChanged(ref buscaPetViewModel, value, nameof(BuscaPetViewModel));
        }
        public PessoaViewModel PessoaViewModel
        {
            get => pessoaViewModel;
            set => OnPropertyChanged(ref pessoaViewModel, value, nameof(PessoaViewModel));
        }
        public PetViewModel PetViewModel
        {
            get => petViewModel;
            set => OnPropertyChanged(ref petViewModel, value, nameof(PetViewModel));
        }
        #endregion

        #region Commands
        private ICommand abrirTelaPessoa;
        private ICommand abrirTelaPet;
        private ICommand abrirTelaRaca;
        private ICommand abrirTelaBuscaPet;
        private ICommand abrirTelaBuscaPessoa;
        private ICommand abrirTelaAdocao;

        public ICommand AbrirTelaAdocao => abrirTelaAdocao?? (abrirTelaAdocao= new RelayCommand(AbreTelaAdocao));
        public ICommand AbrirTelaBuscaPessoa => abrirTelaBuscaPessoa ?? (abrirTelaBuscaPessoa = new RelayCommand(AbreTelaBuscaPessoa));
        public ICommand AbrirTelaPessoa => abrirTelaPessoa ?? (abrirTelaPessoa = new RelayCommand(AbreTelaPessoa));
        public ICommand AbrirTelaPet => abrirTelaPet ?? (abrirTelaPet = new RelayCommand(AbreTelaPet));
        public ICommand AbrirTelaRaca => abrirTelaRaca ?? (abrirTelaRaca = new RelayCommand(AbreTelaRaca));
        public ICommand AbrirTelaBuscaPet => abrirTelaBuscaPet ?? (abrirTelaBuscaPet = new RelayCommand(AbreTelaBuscaPet));
        #endregion

        #region Methods
        public void AbreTelaRaca(object o)
        {
            if (TelaRaca is null)
                TelaRaca = new RacaUI();
            TelaRaca.ShowDialog();
        }
        public void AbreTelaBuscaPet(object o)
        {
            if (telaBuscaPet is null)
                telaBuscaPet = new();
            telaBuscaPet.DataContext = this;
            if(BuscaPetViewModel is null)
                BuscaPetViewModel = new();
            telaBuscaPet.ShowDialog();
        }
        public void AbreTelaAdocao(object o)
        {
            if (telaAdocao is null)
                telaAdocao = new();
            telaAdocao.DataContext = this;
            if (AdocaoViewModel is null)
                AdocaoViewModel = new();
            telaAdocao.ShowDialog();
        }

        public void AbreTelaBuscaPessoa(object o)
        {
            if (telaBuscaPessoa is null)
                telaBuscaPessoa = new();
            telaBuscaPessoa.DataContext = this;
            if (BuscaPessoaViewModel is null)
                BuscaPessoaViewModel = new();
            telaBuscaPessoa.ShowDialog();
        }
        public void AbreTelaPessoa(object o)
        {
            MostraTelaPessoa();
        }

        private void MostraTelaPessoa()
        {
            if (telaPessoa is null)
                telaPessoa = new PessoaUI();
            telaPessoa.DataContext = this;
            if (PessoaViewModel is null)
            {
                PessoaViewModel = new PessoaViewModel();
            }
            else
            {
                //TODO: clear fields
            }
            telaPessoa.ShowDialog();
        }

        public void AbreTelaPet(object o)
        {
            if (telaPet is null)
                telaPet = new PetUI();
            telaPet.DataContext = this;
            if (petViewModel is null)
                PetViewModel = new();
            else
            {

            }
            telaPet.ShowDialog();
        }
        #endregion

        public MainViewModel()
        {
        }
    }
}
