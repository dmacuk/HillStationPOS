
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace HillStationPOS.Model.Entities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public partial class Customer
    {
        readonly Regex _rgx = new Regex("[^a-zA-Z0-9]");

        public string StrippedDetails => _rgx.Replace(Details, "");
    }
}