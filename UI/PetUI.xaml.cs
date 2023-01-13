using System.Windows;
using System.Windows.Controls.Ribbon;
using UI.ViewModel;

namespace UI
{
    /// <summary>
    /// Lógica interna para PetUI.xaml
    /// </summary>
    public partial class PetUI : RibbonWindow 
    {
        public PetUI()
        {
            InitializeComponent();
            Closing += PetUI_Closing;
        }

        private void PetUI_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            ((PetViewModel)DataContext).Dispose();
        }
    }
}
