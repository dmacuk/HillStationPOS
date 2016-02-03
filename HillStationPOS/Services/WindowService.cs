using System.Diagnostics.CodeAnalysis;
using System.Windows;
using HillStationPOS.Interfaces;
using HillStationPOS.Utilities;
using HillStationPOS.Windows;
using HillStationPOS.Windows.ChangePassword;
using HillStationPOS.Windows.Password;

namespace HillStationPOS.Services
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class WindowService : IWindowService
    {
        public bool ModifyMenu()
        {
            var dlg = new MenuMaintenanceWindow();
            dlg.ShowDialog();
            return dlg.ReloadData;
        }

        public bool GetPassword(Window owner)
        {
            var password = PasswordUtilities.GetPassword();
            while (true)
            {
                var dlg = new PasswordDialog {Owner = owner};
                if (dlg.ShowDialog() == true)
                {
                    var response = dlg.Response;
                    if (response == password) return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void ShowUtilitiesMenu(Window window)
        {
            var dlg = new UtilitiesMenuDialog {Owner = window};
            dlg.ShowDialog();
        }

        public void ChangePassword(Window owner)
        {
            var dlg = new ChangePasswordDialog {Owner = owner};
            dlg.ShowDialog();
        }
    }
}