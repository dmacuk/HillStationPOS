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
                    result.AddRange(
                        entities.Headers.OrderBy(h => h.DisplayOrder).ToList().Select(header => new Header(header)));
                }
                return result;
            });
        }
    }
}