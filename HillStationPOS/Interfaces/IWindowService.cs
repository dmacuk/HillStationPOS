using System.Windows;

namespace HillStationPOS.Interfaces
{
    public interface IWindowService
    {
        bool ModifyMenu();
        bool GetPassword(Window owner);
        void ShowUtilitiesMenu(Window window);
        void ChangePassword(Window owner);
    }
}