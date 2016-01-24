using System.Diagnostics.CodeAnalysis;
using HillStationPOS.Interfaces;
using HillStationPOS.Windows;

namespace HillStationPOS.Services
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
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