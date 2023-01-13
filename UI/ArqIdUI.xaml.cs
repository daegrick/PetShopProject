using System.Security.Principal;
using System.Windows;
using System.Windows.Controls.Ribbon;
using UI.ViewModel;

namespace PetShopDesafeto
{
    /// <summary>
    /// Interaction logic for ArqIdUI.xaml
    /// </summary>
    public partial class ArqIDUI : RibbonWindow
    {
        public ArqIDUI()
        {
            InitializeComponent();
            ((ArqIDViewModel)DataContext).CloseAction = Close;
            this.Closing += ArqIDUI_Closing;
        }

        private void ArqIDUI_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((ArqIDViewModel)DataContext).CanProceed)
                return;
            MessageBox.Show("Conexão não configurada! O sistema será encerrado!");
            Application.Current.Shutdown();
        }
    }
}
