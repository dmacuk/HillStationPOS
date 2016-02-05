using System.ComponentModel;
using System.Windows;
using Utils.Window;

namespace HillStationPOS.Windows.Utilities
{
    /// <summary>
    ///     Interaction logic for UtilitiesMenuDialog.xaml
    /// </summary>
    public partial class UtilitiesMenuDialog
    {
        public UtilitiesMenuDialog()
        {
            InitializeComponent();
        }

        private void ExitProgram(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            this.SaveSettings(false);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.LoadSettings(false);
        }
    }
}