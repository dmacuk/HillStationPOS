using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HillStationPOS.Interfaces;

namespace HillStationPOS.Windows.Utilities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UtilitiesMenuModel : ViewModelBase
    {
        private readonly IWindowService _windowService;

        public UtilitiesMenuModel(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public ICommand ChangePasswordCommand => new RelayCommand<Window>(ChangePassword);

        public ICommand MaintainCustomersCommand => new RelayCommand<Window>(MaintainCustomers);
        public ICommand UpdatemenuCommand => new RelayCommand<Window>(UpdateMenu);

        private void ChangePassword(Window owner)
        {
            _windowService.ChangePassword(owner);
        }

        private void MaintainCustomers(Window owner)
        {
            _windowService.MaintainCustomers(owner);
        }

        private void UpdateMenu(Window window)
        {
        }
    }
}