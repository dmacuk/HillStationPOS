using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HillStationPOS.Interfaces;

namespace HillStationPOS.Windows.MainMenu
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainMenuModel : ViewModelBase
    {
        private readonly IWindowService _windowService;

        public MainMenuModel(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public ICommand AddOrderCommand => new RelayCommand(AddOrder);
        public ICommand UtilitiesCommand => new RelayCommand<Window>(Utilities);

        private void AddOrder()
        {
            throw new NotImplementedException();
        }

        private void Utilities(Window window)
        {
            if (_windowService.GetPassword(window))
            {
                _windowService.ShowUtilitiesMenu(window);
            }
        }
    }
}