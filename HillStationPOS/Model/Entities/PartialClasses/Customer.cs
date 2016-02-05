using System;
using System.Text;
using System.Text.RegularExpressions;
using PropertyChanged;

// ReSharper disable once CheckNamespace

namespace HillStationPOS.Model.Entities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [ImplementPropertyChanged]
    public partial class Customer : IComparable<Customer>
    {
        public static readonly Regex Rgx = new Regex("[^a-zA-Z0-9]");

        public string Details
        {
            get
            {
                var sb = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(Name)) sb.Append(Name).Append(Environment.NewLine);
                if (!string.IsNullOrWhiteSpace(Address)) sb.Append(Address).Append(Environment.NewLine);
                if (!string.IsNullOrWhiteSpace(Postcode)) sb.Append(Postcode).Append(Environment.NewLine);
                if (!string.IsNullOrWhiteSpace(Phone)) sb.Append(Phone).Append(Environment.NewLine);
                return sb.ToString();
            }
        }

        public string StrippedDetails => Rgx.Replace(Details, "").ToLower();

        public int CompareTo(Customer other)
        {
            var result = string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
            if (result == 0) result = string.Compare(Address, other.Address, StringComparison.OrdinalIgnoreCase);
            if (result == 0) result = string.Compare(Postcode, other.Postcode, StringComparison.OrdinalIgnoreCase);
            if (result == 0) result = string.Compare(Phone, other.Phone, StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Address: {Address}, Postcode: {Postcode}, Phone: {Phone}";
        }
    }
}