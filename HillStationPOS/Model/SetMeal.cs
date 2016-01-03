namespace HillStationPOS.Model
{
    public class SetMeal : AbstractOrderItem
    {
        public decimal Ajustment { get; set; }

        public override decimal Price
        {
            get { return base.Price + Ajustment; }
            set { Set("Price", ref _price, value); }
        }
    }


    public enum ChickenStarterType
    {
        Pakora,
        Tikka
    }

    public enum SetMealType
    {
        One,
        Two
    }
}