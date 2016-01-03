using HillStationPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HillStationPOS.Windows
{
    /// <summary>
    /// Interaction logic for MenuMaintenanceWindow.xaml
    /// </summary>
    public partial class MenuMaintenanceWindow : Window
    {

        public bool ReloadData { get; set; }

        public MenuMaintenanceWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MenuViewModel model = (MenuViewModel)DataContext;
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
