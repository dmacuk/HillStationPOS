using System.Collections.Generic;
using System.Threading.Tasks;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Interfaces
{
    public interface IDataService
    {
        string GetSetMealIdentifier();
        Task<List<Header>> LoadMenuAsync();
        Task<List<Customer>> LoadCustomersAsync();
        Customer AddCustomer(Customer customer);
    }
}