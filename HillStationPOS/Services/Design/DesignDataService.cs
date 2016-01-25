using System.Collections.Generic;
using System.Threading.Tasks;
using HillStationPOS.Interfaces;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Services.Design
{
    public class DesignDataService : IDataService
    {
        public string GetSetMealIdentifier()
        {
            return "Set Meal";
        }

        public Task<List<Header>> LoadMenuAsync()
        {
            return null;
        }

        public Task<List<Customer>> LoadCustomersAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}