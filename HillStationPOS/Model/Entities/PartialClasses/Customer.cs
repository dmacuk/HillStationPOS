using System.Text.RegularExpressions;
using PropertyChanged;

// ReSharper disable once CheckNamespace

namespace HillStationPOS.Model.Entities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [ImplementPropertyChanged]
    public partial class Customer
    {
        private readonly Regex _rgx = new Regex("[^a-zA-Z0-9]");

        public string StrippedDetails => _rgx.Replace(Details, "");
    }
}