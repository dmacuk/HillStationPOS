using System.Linq;
using PropertyChanged;

// ReSharper disable once CheckNamespace

namespace HillStationPOS.Model.Entities
{
    [ImplementPropertyChanged]
    public partial class Header
    {
        public Header(Header header) : this()
        {
            Id = header.Id;
            Title = header.Title;
            DisplayOrder = header.DisplayOrder;
            foreach (var meal in header.Meals.OrderBy(m => m.DisplayOrder))
            {
                // ReSharper disable once VirtualMemberCallInContructor
                Meals.Add(new Meal(meal));
            }
        }
    }
}