using System.Collections.Generic;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Interfaces
{
    public interface IDataService
    {
        List<Customer> Customers { get; set; }

        List<Header> Headers { get; set; }

        Customer AddCustomer(Customer customer);

        void RemoveCustomer(Customer customer);

        void UpdateCustomer(Customer customer);
    }
}