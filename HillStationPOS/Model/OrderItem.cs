using System.Diagnostics;

namespace HillStationPOS.Model
{
    public class OrderItem : AbstractOrderItem
    {
        public override string Notes
        {
            get { return _notes; }
            set
            {
                Debug.WriteLine("Setting notes 1");
                Set("Notes", ref _notes, value);
            }
        }

    }
}