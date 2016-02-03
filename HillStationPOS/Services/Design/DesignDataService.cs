using System;
using System.Collections.Generic;
using HillStationPOS.Interfaces;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Services.Design
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DesignDataService : IDataService
    {
        // ReSharper disable once EmptyConstructor
        public DesignDataService()
        {
//            var orderItem = new OrderItem
//            {
//                Description = "Meal Description",
//                Price = 10.95M,
//                Notes = "These are meal notes"
//            };
//            OrderItems.Add(orderItem);
//
//            var header = new Header { Title = "Starters" };
//            Headers.Add(header);
//
//            var meal = new Meal
//            {
//                Price = 5m,
//                ChickenPrice = 5m,
//                KingPrawnPrice = 5m,
//                LambPrice = 5m,
//                PrawnPrice = 5m,
//                VegetablePrice = 5m,
//                Title = "This is a meal"
//            };
//            Meals.Add(meal);
//            OrderNumber = "A0001";
//            Address = "David McCallum" + Environment.NewLine + "10 Bingham Broadway" + Environment.NewLine +
//                      "EH15 3JL" + Environment.NewLine + "07757 438 032";
        }

        public List<Customer> Customers { get; set; }
        public List<Header> Headers { get; set; }

        public Customer AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}