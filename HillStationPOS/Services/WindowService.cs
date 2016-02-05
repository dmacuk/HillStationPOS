using HillStationPOS.Interfaces;
using HillStationPOS.Utilities;
using HillStationPOS.Windows;
using HillStationPOS.Windows.ChangePassword;
using HillStationPOS.Windows.MaintainCustomers;
using HillStationPOS.Windows.Password;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using UtilitiesMenuDialog = HillStationPOS.Windows.Utilities.UtilitiesMenuDialog;

namespace HillStationPOS.Services
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class WindowService : IWindowService
    {
        public void ChangePassword(Window owner)
        {
            var dlg = new ChangePasswordDialog { Owner = owner };
            dlg.ShowDialog();
        }

        public bool GetPassword(Window owner)
        {
            var password = PasswordUtilities.GetPassword();
            while (true)
            {
                var dlg = new PasswordDialog { Owner = owner };
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

        public void MaintainCustomers(Window owner)
        {
            var dlg = new MaintainCustomersDialog { Owner = owner };
            dlg.ShowDialog();
        }

        public bool ModifyMenu()
        {
            var dlg = new MenuMaintenanceWindow();
            dlg.ShowDialog();
            return dlg.ReloadData;
        }

        public void ShowUtilitiesMenu(Window window)
        {
            var dlg = new UtilitiesMenuDialog { Owner = window };
            dlg.ShowDialog();
        }
    }
}