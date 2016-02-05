using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using HillStationPOS.Interfaces;
using HillStationPOS.Model.Entities;
using HillStationPOS.Services;

namespace HillStationPOS.ViewModel
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomerPickerModel : ViewModelBase
    {
        private readonly List<Customer> _allCustomers;
        private readonly IDataService _dataService;
        private string _address;
        private List<Customer> _customers;

        public CustomerPickerModel()
        {
            _dataService = new DataService();
            _allCustomers = _dataService.Customers;
            if (IsInDesignMode)
            {
                for (var i = 0; i < 10; i++)
                {
                    //                    _allCustomers.Add(new Customer {Details = CustomerBuilder.BuildCustomer()});
                }
                _allCustomers.Sort();
                Customers = new List<Customer>(_allCustomers);
            }
            else
            {
                LoadCustomers();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (Set("Address", ref _address, value))
                {
                    var strippedValue = Customer.Rgx.Replace(value, "").ToLower();
                    Customers =
                        new List<Customer>(
                            _allCustomers.Where(c => c.StrippedDetails.Contains(strippedValue)));
                }
            }
        }

        public List<Customer> Customers
        {
            get { return _customers; }
            set { Set("Customers", ref _customers, value); }
        }

        public Customer Add(Customer customer)
        {
            var added = _dataService.AddCustomer(customer);
            _allCustomers.Add(added);
            _allCustomers.Sort();
            return added;
        }

        private void LoadCustomers()
        {
            _allCustomers.AddRange(_dataService.Customers);
            Customers = new List<Customer>(_allCustomers);
        }
    }
}