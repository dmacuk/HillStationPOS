using System;
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

        public string StrippedDetails => Rgx.Replace(Details, "").ToLower();

        public int CompareTo(Customer other)
        {
            return string.Compare(Details, other.Details, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return Details;
        }
    }
}