using System.Collections.Generic;
using System.Threading.Tasks;
using HillStationPOS.Interfaces;
using HillStationPOS.Model.Entities;
using System.Collections.ObjectModel;
using System.Linq;

namespace HillStationPOS.Services
{
    internal class DataService : IDataService
    {
        public static long SetMealIdentifier;

        public string GetSetMealIdentifier()
        {
            return "Set Meal";
        }

        public async Task<List<Header>> LoadMenuAsync()
        {
            return await Task.Factory.StartNew(() =>
            {

                var result = new List<Header>();
                using (var entities = new HillStationEntities())
                {
                    foreach (var header in entities.Headers.OrderBy(h => h.DisplayOrder).ToList())
                    {
                        result.Add(new Header(header));
                    }
                }
                return result;
            });
        }
    }
}