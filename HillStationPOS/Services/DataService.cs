using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HillStationPOS.Interfaces;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class DataService : IDataService
    {
        private static DataService _instance;
        public static DataService Instance => _instance ?? (_instance = new DataService());
        public List<Customer> Customers { get; set; }
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

        public Task<bool> Initialise()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var entities = new HillStationEntities())
                {
                    Headers =
                        new List<Header>(
                            entities.Headers.OrderBy(h => h.DisplayOrder).ToList().Select(header => new Header(header)));
                    Customers = new List<Customer>(entities.Customers.OrderBy(c => c.Details));
                }
                return true;
            });
        }
    }
}