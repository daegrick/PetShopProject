using System.Windows.Controls.Ribbon;
using UI.ViewModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for PessoaSearchUI.xaml
    /// </summary>
    public partial class PessoaSearchUI :RibbonWindow
    {
        public PessoaSearchUI()
        {
            InitializeComponent();
            Closing += PessoaSearchUI_Closing;
        }

        private void PessoaSearchUI_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            ((BuscaPessoaViewModel)DataContext).Dispose();
        }
    }
}
