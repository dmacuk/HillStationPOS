using System.Diagnostics;
using System.Windows;
using HillStationPOS.Interfaces;
using HillStationPOS.Model.Entities;
using HillStationPOS.Services;

namespace HillStationPOS.UserControls
{
    /// <summary>
    ///     Interaction logic for ListTest.xaml
    /// </summary>
    public partial class ListTest : Window
    {
        private int _count;

        public ListTest()
        {
            InitializeComponent();
            IDataService service = new DataService();
            CustomerList.Initialise(service.Customers);
            CustomerList.SelectedCustomerChangedEvent += CustomerChanged;
        }

        private static void CustomerChanged(object sender, CustomerListEventArgs customer)
        {
            Debug.WriteLine(customer.Customer);
        }

        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            var customer = new Customer {Name = $"Dame {_count++}"};
            CustomerList.AddCustomer(customer);
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            var customer = CustomerList.SelectedCustomer;
            if (customer != null) CustomerList.DeleteCustomer(customer);
        }
    }
}