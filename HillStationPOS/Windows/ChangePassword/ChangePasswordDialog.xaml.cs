using System.Windows;
using HillStationPOS.Utilities;

namespace HillStationPOS.Windows.ChangePassword
{
    /// <summary>
    ///     Interaction logic for ChangePasswordDialog.xaml
    /// </summary>
    public partial class ChangePasswordDialog
    {
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            var savedPassword = PasswordUtilities.GetPassword();
            var oldPassword = TextOldPassword.Password;
            var newPassword = TextNewPassword.Password;
            var newPasswordRepeat = TextNewPasswordRepeat.Password;
            if (savedPassword != oldPassword || string.IsNullOrWhiteSpace(newPassword) || newPassword != newPasswordRepeat)
            {
                MessageBox.Show("Either the old password is wrong, or the new password entries do not match");
                return;
            }
            PasswordUtilities.SetPassword(newPassword);
            if (Owner != null) DialogResult = true;
        }
    }
}