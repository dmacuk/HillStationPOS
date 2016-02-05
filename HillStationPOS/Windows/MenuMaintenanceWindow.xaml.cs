using HillStationPOS.ViewModel;
using System.Windows;

namespace HillStationPOS.Windows
{
    /// <summary>
    ///     Interaction logic for MenuMaintenanceWindow.xaml
    /// </summary>
    public partial class MenuMaintenanceWindow : Window
    {
        public MenuMaintenanceWindow()
        {
            InitializeComponent();
        }

        public bool ReloadData { get; set; }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var model = (MenuViewModel)DataContext;
            model.MenuUpdated += Model_MenuUpdated;
        }

        private void Model_MenuUpdated(object sender, MenuUpdatedEventArgs e)
        {
            switch (e.UpdateOperation)
            {
                case UpdateOperation.Updated:
                    ReloadData = true;
                    break;

                case UpdateOperation.Cancelled:
                    ReloadData = false;
                    break;

                default:
                    break;
            }
            Close();
        }
    }
}