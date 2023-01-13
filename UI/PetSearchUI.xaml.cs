using System.Windows.Controls.Ribbon;
using UI.ViewModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for PetSearchUI.xaml
    /// </summary>
    public partial class PetSearchUI : RibbonWindow
    {
        public PetSearchUI()
        {
            InitializeComponent();
            Closing += PetSearchUI_Closing;
        }

        private void PetSearchUI_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            ((BuscaPetViewModel)DataContext).Dispose();
        }
    }
}
