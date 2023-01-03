using System.ComponentModel;
using System.Windows.Controls.Ribbon;

namespace UI
{
    /// <summary>
    /// Interaction logic for AdocaoUI.xaml
    /// </summary>
    public partial class AdocaoUI : RibbonWindow
    {
        public AdocaoUI()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }

    }
}
