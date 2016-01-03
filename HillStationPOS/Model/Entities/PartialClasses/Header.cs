using System.Linq;

// ReSharper disable once CheckNamespace

namespace HillStationPOS.Model.Entities
{
    public partial class Header
    {
        public Header(Header header) : this()
        {
            Id = header.Id;
            Title = header.Title;
            DisplayOrder = header.DisplayOrder;
            foreach (var meal in header.Meals.OrderBy(m => m.DisplayOrder))
            {
                Meals.Add(new Meal(meal));
            }
        }
    }
}