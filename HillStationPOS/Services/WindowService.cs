using HillStationPOS.Interfaces;
using HillStationPOS.Windows;

namespace HillStationPOS.Services
{
    internal class WindowService : IWindowService
    {
        public bool ModifyMenu()
        {
            MenuMaintenanceWindow dlg = new MenuMaintenanceWindow();
            var result = dlg.ShowDialog();
            return dlg.ReloadData;
        }
    }
}