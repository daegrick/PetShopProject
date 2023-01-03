using BLL.Controle;
using DAL;
using DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UI.ViewModel
{
    public class BuscaPessoaViewModel : ViewModelBase
    {
        #region Setters
        private string pesquisaNome;
        private ObservableCollection<Pessoa> pessoas;
        public ObservableCollection<Pessoa> Pessoas
        {
            get => pessoas;
            set => OnPropertyChanged(ref pessoas, value, nameof(Pessoas));
        }
        public string PesquisaNome
        {
            get => pesquisaNome;
            set => OnPropertyChanged(ref pesquisaNome, value, nameof(PesquisaNome));
        }
        #endregion
        #region Commands
        private ICommand buscaPessoaCommand;
        public ICommand BuscaPessoaCommand => buscaPessoaCommand ?? (buscaPessoaCommand = new RelayCommand(BuscaPessoa));
        #endregion

        #region Methods
        public void BuscaPessoa(object o = null)
        {
            var pessoas = PessoaBLL.ListaPessoas();
            foreach(var pessoa in pessoas)
            {
                Pessoas.Add(pessoa);
            }
        }
        #endregion
        #region Constructor
        public BuscaPessoaViewModel() {
           
            pesquisaNome = string.Empty;
            Pessoas = new();
            BuscaPessoa();
        }
        #endregion
    }
}