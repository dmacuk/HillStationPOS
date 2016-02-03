using HillStationPOS.Model.Entities;
using System.Collections.Generic;

namespace HillStationPOS.Interfaces
{
    public interface IDataService
    {
        List<Customer> Customers { get; set; }

        List<Header> Headers { get; set; }

        Customer AddCustomer(Customer customer);
    }
}