using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HillStationPOS.Model.Entities;
using HillStationPOS.ViewModel;

namespace HillStationPOS.Windows
{
    public partial class CutomerPickerWindow : Window
    {
        public CutomerPickerWindow()
        {
            InitializeComponent();
        }

        public Customer Customer { get; set; }

        private void SelectCustomer(object sender, MouseButtonEventArgs e)
        {
            Customer = (Customer) ((ListBox) sender).SelectedItem;
        }

        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            var customer = new Customer {Details = TextCustomer.Text};
            var model = (CustomerPickerModel) DataContext;
            var added = model.Add(customer);
            Customer = added;
        }
    }
}