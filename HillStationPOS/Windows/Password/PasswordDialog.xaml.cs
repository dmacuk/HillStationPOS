using System.Windows;

namespace HillStationPOS.Windows.Password
{
    /// <summary>
    ///     Interaction logic for PasswordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : Window
    {
        public PasswordDialog()
        {
            InitializeComponent();
        }

        public string Response => ResponseTextBox.Password;

        private void OkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}