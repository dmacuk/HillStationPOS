using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HillStationPOS.Interfaces;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Windows.MaintainCustomers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MaintainCustomersModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private bool _adding;
        private string _address;
        private bool _isEditing;
        private string _name;
        private bool _notEditing;
        private string _phone;
        private string _postcode;
        private Customer _selectedCustomer;

        public MaintainCustomersModel(IDataService dataService)
        {
            _dataService = dataService;
            Customers = new ObservableCollection<Customer>(_dataService.Customers);
            Customers.CollectionChanged += UpdateDatabase;
            if (Customers.Count > 0) SelectedCustomer = Customers[0];
            IsEditing = false;
            NotEditing = true;
        }

        public ICommand AddCommand => new RelayCommand(Add);

        public string Address
        {
            get { return _address; }
            set { Set("Address", ref _address, value); }
        }

        public ICommand CancelCommand => new RelayCommand(Cancel);

        public ObservableCollection<Customer> Customers { get; set; }

        public ICommand DeleteCommand => new RelayCommand(Delete);

        public ICommand EditCommand => new RelayCommand(Edit);

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                if (Set("IsEditing", ref _isEditing, value))
                {
                    NotEditing = !value;
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set { Set("Name", ref _name, value); }
        }

        public bool NotEditing
        {
            get { return _notEditing; }
            set { Set("NotEditing", ref _notEditing, value); }
        }

        public string Phone
        {
            get { return _phone; }
            set { Set("Phone", ref _phone, value); }
        }

        public string Postcode
        {
            get { return _postcode; }
            set { Set("Postcode", ref _postcode, value); }
        }

        public ICommand SaveCommand => new RelayCommand(Save);

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (Set("SelectedCustomer", ref _selectedCustomer, value))
                {
                    SetFields(value);
                }
            }
        }

        private void Add()
        {
            _adding = true;
            SetFields(null);
            IsEditing = true;
        }

        private void AddCustomer(Customer customer)
        {
            var count = 0;
            var added = false;
            foreach (var current in Customers)
            {
                if (customer.CompareTo(current) < 0)
                {
                    Customers.Insert(count, customer);
                    added = true;
                    break;
                }
                count++;
            }
            if (!added) Customers.Add(customer);
        }

        private void Cancel()
        {
            SetFields(SelectedCustomer);
            IsEditing = false;
        }

        private void Delete()
        {
            if (SelectedCustomer == null) return;
            if (
                MessageBox.Show($"Delete {SelectedCustomer.Name}?", "Alert", MessageBoxButton.YesNo,
                    MessageBoxImage.Stop) == MessageBoxResult.Yes)
                Customers.Remove(SelectedCustomer);
        }

        private void Edit()
        {
            _adding = false;
            IsEditing = true;
        }

        private void Save()
        {
            // Save/Update the customer
            // First detach so we don't update the database
            var customer = _adding ? new Customer() : SelectedCustomer;
            customer.Name = Name;
            customer.Address = Address;
            customer.Postcode = Postcode;
            customer.Phone = Phone;
            if (_adding)
            {
                AddCustomer(customer);
                SelectedCustomer = customer;
            }
            IsEditing = false;
        }

        private void SetFields(Customer value)
        {
            if (value == null)
            {
                Name = string.Empty;
                Address = string.Empty;
                Postcode = string.Empty;
                Phone = string.Empty;
            }
            else
            {
                Name = value.Name;
                Address = value.Address;
                Postcode = value.Postcode;
                Phone = value.Phone;
            }
        }

        private void UpdateDatabase(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    _dataService.AddCustomer((Customer) e.NewItems[0]);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    _dataService.RemoveCustomer((Customer) e.OldItems[0]);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}