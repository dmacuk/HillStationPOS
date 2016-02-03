using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _allCustomers = new List<Customer>();
            if (IsInDesignMode)
            {
                for (var i = 0; i < 10; i++)
                {
                    _allCustomers.Add(new Customer {Details = CustomerBuilder.BuildCustomer()});
                }
                _allCustomers.Sort();
                Customers = new List<Customer>(_allCustomers);
            }
            else
            {
                LoadCustomers();
            }
        }

        public List<Customer> Customers
        {
            get { return _customers; }
            set { Set("Customers", ref _customers, value); }
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

        private void LoadCustomers()
        {
            _allCustomers.AddRange(_dataService.Customers);
            Customers = new List<Customer>(_allCustomers);
        }

        public Customer Add(Customer customer)
        {
            var added = _dataService.AddCustomer(customer);
            _allCustomers.Add(added);
            _allCustomers.Sort();
            return added;
        }
    }

    internal static class CustomerBuilder
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);
        private static readonly string[] Names = {"David McCallum", "Charles McCallum", "Grant Butchart", "Arif Khan"};

        private static readonly string[] Addresses =
        {
            "10 Bingham Broadway", "40 Balfour Street", "49 Canaan Lane",
            "101 Comiston Road"
        };

        private static readonly string[] PostCodes = {"EH15 3JL", "EH7 9XX", "EH10 7YY", "EH10 6ZZ"};
        private static readonly string[] PhoneNumbers = {"07757 438 032", "0131 620 2968", "999 9999", "123 4567"};


        public static string BuildCustomer()
        {
            var customer = new StringBuilder();
            customer.Append(RandomLine(Names)).Append(Environment.NewLine);
            customer.Append(RandomLine(Addresses)).Append(Environment.NewLine);
            customer.Append(RandomLine(PostCodes)).Append(Environment.NewLine);
            customer.Append(RandomLine(PhoneNumbers));
            return customer.ToString();
        }

        private static string RandomLine(string[] lines)
        {
            return lines[Random.Next(lines.Length)];
        }
    }
}