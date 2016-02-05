/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:HillStationPOS.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using HillStationPOS.Interfaces;
using HillStationPOS.Services;
using HillStationPOS.Services.Design;
using HillStationPOS.Windows.MainMenu;
using HillStationPOS.Windows.MaintainCustomers;
using HillStationPOS.Windows.Utilities;
using Microsoft.Practices.ServiceLocation;

namespace HillStationPOS.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    ///     <para>
    ///         See http://www.mvvmlight.net
    ///     </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
                SimpleIoc.Default.Register<IWindowService, DesignWindowService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
                SimpleIoc.Default.Register<IWindowService, WindowService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<CustomerPickerModel>();
            SimpleIoc.Default.Register<MainMenuModel>();
            SimpleIoc.Default.Register<UtilitiesMenuModel>();
            SimpleIoc.Default.Register<MaintainCustomersModel>();
        }

        // ReSharper disable MemberCanBeMadeStatic.Global
        public CustomerPickerModel Customers => ServiceLocator.Current.GetInstance<CustomerPickerModel>();

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public MainMenuModel MainMenuModel => ServiceLocator.Current.GetInstance<MainMenuModel>();

        public MaintainCustomersModel MaintainCustomersModel
            => ServiceLocator.Current.GetInstance<MaintainCustomersModel>();

        public MenuViewModel Menu => ServiceLocator.Current.GetInstance<MenuViewModel>();

        public UtilitiesMenuModel UtilitiesMenuModel => ServiceLocator.Current.GetInstance<UtilitiesMenuModel>();

        /// <summary>
        ///     Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }

        // ReSharper restore MemberCanBeMadeStatic.Global
    }
}