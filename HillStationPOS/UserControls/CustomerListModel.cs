using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.UserControls
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomerListModel : ViewModelBase
    {
        public delegate void SelectedCustomerChangedEventHandler(object sender, CustomerListEventArgs e);

        private readonly List<Customer> _data;

        private ListCollectionView _customers;

        private string _filter;

        public CustomerListModel(List<Customer> data)
        {
            _data = data;
            _customers = (ListCollectionView) CollectionViewSource.GetDefaultView(_data);
            _customers.Filter = FilterCustomers;
            _customers.CurrentChanged += CustomerChanged;
            _customers.CustomSort = new CustomerSorter();
            _customers.MoveCurrentToPosition(-1);
        }

        public ListCollectionView Customers
        {
            get { return _customers; }
            set { Set("Customers", ref _customers, value); }
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (Set("Filter", ref _filter, value))
                {
                    Customers.Refresh();
                }
            }
        }

        public Customer SelectedCustomer { get; private set; }

        public event SelectedCustomerChangedEventHandler SelectedCustomerChangedEvent;

        public void AddCustomer(Customer customer)
        {
            _data.Add(customer);
            Customers.Refresh();
            Customers.MoveCurrentTo(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            _data.Remove(customer);
            Customers.Refresh();
        }

        private void OnSelectedCustomerChangedEvent(Customer customer)
        {
            SelectedCustomerChangedEvent?.Invoke(this, new CustomerListEventArgs {Customer = customer});
        }

        private void CustomerChanged(object sender, EventArgs e)
        {
            SelectedCustomer = (Customer) Customers.CurrentItem;
            OnSelectedCustomerChangedEvent(SelectedCustomer);
        }

        private bool FilterCustomers(object item)
        {
            var customer = item as Customer;
            if (customer == null) return true;
            return string.IsNullOrWhiteSpace(_filter) ||
                   customer.StrippedDetails.Contains(Customer.Rgx.Replace(_filter, ""));
        }
    }

    public class CustomerSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var custX = (Customer) x;
            var custY = (Customer) y;
            return custX.CompareTo(custY);
        }
    }
}