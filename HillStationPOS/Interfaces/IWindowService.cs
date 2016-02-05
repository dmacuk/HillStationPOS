using System.Windows;

namespace HillStationPOS.Interfaces
{
    public interface IWindowService
    {
        void ChangePassword(Window owner);

        bool GetPassword(Window owner);

        void MaintainCustomers(Window owner);

        bool ModifyMenu();

        void ShowUtilitiesMenu(Window window);
    }
}