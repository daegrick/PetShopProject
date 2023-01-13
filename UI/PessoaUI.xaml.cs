using System.Windows.Controls.Ribbon;
using UI.ViewModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for PessoaUI.xaml
    /// </summary>
    public partial class PessoaUI : RibbonWindow
    {
        public PessoaUI()
        {
            InitializeComponent();
            Closing += PessoaUI_Closing;
        }

        private void PessoaUI_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            ((PessoaViewModel)DataContext).Dispose();
        }
    }
}
