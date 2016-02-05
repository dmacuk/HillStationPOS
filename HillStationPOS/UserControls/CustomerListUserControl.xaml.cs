using System.Collections.Generic;
using System.Windows;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.UserControls
{
    /// <summary>
    ///     Interaction logic for CustomerListUserControl.xaml
    /// </summary>
    public partial class CustomerListUserControl
    {
        public delegate void SelectedCustomerChangedEventHandler(object sender, CustomerListEventArgs e);

        private CustomerListModel _model;

        public CustomerListUserControl()
        {
            InitializeComponent();
        }

        public Customer SelectedCustomer => _model.SelectedCustomer;

        public event SelectedCustomerChangedEventHandler SelectedCustomerChangedEvent;

        public void AddCustomer(Customer customer)
        {
            _model.AddCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            _model.DeleteCustomer(customer);
        }

        private void OnSelectedCustomerChangedEvent(CustomerListEventArgs customer)
        {
            SelectedCustomerChangedEvent?.Invoke(this, customer);
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void SelectedCustomerChanged(object sender, CustomerListEventArgs customer)
        {
            OnSelectedCustomerChangedEvent(customer);
        }

        public void Initialise(List<Customer> customers)
        {
            _model = new CustomerListModel(customers);
            DataContext = _model;
            _model.SelectedCustomerChangedEvent += SelectedCustomerChanged;
        }
    }
}