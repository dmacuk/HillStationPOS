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
        }

        public List<Customer> Customers
        {
            get
            {
                var customers = new List<Customer>();
                for (var i = 0; i < 10; i++)
                {
                    customers.Add(CustomerBuilder.BuildCustomer());
                }
                customers.Sort();
                return customers;
            }
            set { throw new NotImplementedException(); }
        }

        public List<Header> Headers { get; set; }

        public Customer AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }

    internal static class CustomerBuilder
    {
        private static readonly string[] Addresses =
        {
            "10 Bingham Broadway", "40 Balfour Street", "49 Canaan Lane",
            "101 Comiston Road"
        };

        private static readonly string[] Names = {"David McCallum", "Charles McCallum", "Grant Butchart", "Arif Khan"};
        private static readonly string[] PhoneNumbers = {"07757 438 032", "0131 620 2968", "999 9999", "123 4567"};
        private static readonly string[] PostCodes = {"EH15 3JL", "EH7 9XX", "EH10 7YY", "EH10 6ZZ"};
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public static Customer BuildCustomer()
        {
            return new Customer
            {
                Name = RandomLine(Names),
                Address = RandomLine(Addresses),
                Postcode = RandomLine(PostCodes),
                Phone = RandomLine(PhoneNumbers)
            };
        }

        private static string RandomLine(string[] lines)
        {
            return lines[Random.Next(lines.Length)];
        }
    }
}