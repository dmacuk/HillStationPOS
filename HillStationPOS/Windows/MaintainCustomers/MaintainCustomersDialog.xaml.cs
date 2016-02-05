using System.Windows;
using System.Windows.Controls;

namespace HillStationPOS.Windows.MaintainCustomers
{
    /// <summary>
    ///     Interaction logic for MaintainCustomersDialog.xaml
    /// </summary>
    public partial class MaintainCustomersDialog : Window
    {
        public MaintainCustomersDialog()
        {
            InitializeComponent();
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateList(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox) sender;
            if (e.AddedItems.Count == 0) return;
            var currentItem = e.AddedItems[0];
            listBox.ScrollIntoView(currentItem);
        }
    }
}