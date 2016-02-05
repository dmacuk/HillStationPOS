using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HillStationPOS.Interfaces;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class DataService : IDataService
    {
        private List<Customer> _customers;

        public List<Customer> Customers
        {
            get
            {
                if (_customers == null)
                {
                    Debug.WriteLine("Loading customers");
                    using (var entities = new HillStationEntities())
                    {
                        _customers = new List<Customer>(entities.Customers.OrderBy(c => c.Name));
                    }
                }
                else
                {
                    Debug.WriteLine("Customers already loaded");
                }
                return _customers;
            }
            set { _customers = value; }
        }

        public List<Header> Headers { get; set; }

        public Customer AddCustomer(Customer customer)
        {
            using (var entities = new HillStationEntities())
            {
                entities.Customers.Add(customer);
                entities.SaveChanges();
                entities.Entry(customer).Reload();
                return customer;
            }
        }

        public void RemoveCustomer(Customer customer)
        {
            using (var entities = new HillStationEntities())
            {
                var customerEntity = entities.Customers.Find(customer.Id);
                entities.Customers.Remove(customerEntity);
                entities.SaveChanges();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var entities = new HillStationEntities())
            {
                var customerEntity = entities.Customers.Find(customer.Id);
                customerEntity.Name = customer.Name;
                customerEntity.Address = customer.Address;
                customerEntity.Postcode = customer.Postcode;
                customerEntity.Phone = customer.Phone;
                entities.SaveChanges();
            }
        }

        private Task<bool> Initialise()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var entities = new HillStationEntities())
                {
                    Headers =
                        new List<Header>(
                            entities.Headers.OrderBy(h => h.DisplayOrder).ToList().Select(header => new Header(header)));
                    Customers = new List<Customer>(entities.Customers.OrderBy(c => c.Name));
                }
                return true;
            });
        }
    }
}