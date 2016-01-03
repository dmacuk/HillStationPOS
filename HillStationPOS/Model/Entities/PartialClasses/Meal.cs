using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows;
using System.Windows.Input;

namespace HillStationPOS.Model.Entities
{
    public partial class Meal
    {

        public delegate void MealAddedEventHandler(object sender, MealAddedEventArgs e);

        public Meal(Meal meal)
        {
            Id = meal.Id;
            HeaderId = meal.HeaderId;
            MealNumber = meal.MealNumber;
            Title = meal.Title;
            Price = meal.Price;
            ChickenPrice = meal.ChickenPrice;
            LambPrice = meal.LambPrice;
            VegetablePrice = meal.VegetablePrice;
            PrawnPrice = meal.PrawnPrice;
            KingPrawnPrice = meal.KingPrawnPrice;
            DisplayOrder = meal.DisplayOrder;
        }

        public Visibility PriceVisible => Price == 0 ? Visibility.Hidden : Visibility.Visible;
        public Visibility ChickenVisible => ChickenPrice == 0 ? Visibility.Hidden : Visibility.Visible;
        public Visibility LambVisible => LambPrice == 0 ? Visibility.Hidden : Visibility.Visible;
        public Visibility VegetableVisible => VegetablePrice == 0 ? Visibility.Hidden : Visibility.Visible;
        public Visibility PrawnVisible => PrawnPrice == 0 ? Visibility.Hidden : Visibility.Visible;
        public Visibility KingPrawnVisible => KingPrawnPrice == 0 ? Visibility.Hidden : Visibility.Visible;

        public ICommand AddMeal
        {
            get
            {
                return new RelayCommand<string>(mealTypeString =>
                {
                    var mealType = (MealType)Enum.Parse(typeof(MealType), mealTypeString);
                    OnMealAdded(new MealAddedEventArgs { MealType = mealType });
                });
            }
        }

        public event MealAddedEventHandler MealAdded;

        protected virtual void OnMealAdded(MealAddedEventArgs e)
        {
            MealAdded?.Invoke(this, e);
        }

        public decimal GetPrice(MealType mealType)
        {
            decimal price;
            switch (mealType)
            {
                case MealType.Price:
                    price = Price;
                    break;

                case MealType.Chicken:
                    price = ChickenPrice;
                    break;

                case MealType.Lamb:
                    price = LambPrice;
                    break;

                case MealType.Vegetable:
                    price = VegetablePrice;
                    break;

                case MealType.Prawn:
                    price = PrawnPrice;
                    break;

                case MealType.KingPrawn:
                    price = KingPrawnPrice;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            return price;
        }

        public string GetDescripion(MealType mealType)
        {
            string prefix;
            switch (mealType)
            {
                case MealType.Price:
                    prefix = string.Empty;
                    break;

                case MealType.Chicken:
                    prefix = "Chicken ";
                    break;

                case MealType.Lamb:
                    prefix = "Lamb ";
                    break;

                case MealType.Vegetable:
                    prefix = "Veg. ";
                    break;

                case MealType.Prawn:
                    prefix = "Prawn ";
                    break;

                case MealType.KingPrawn:
                    prefix = "King Prawn ";
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            return prefix + Title;
        }

    }
}

public class MealAddedEventArgs : EventArgs
{
    public MealType MealType { get; set; }
}

public enum MealType
{
    Price,
    Chicken,
    Lamb,
    Vegetable,
    Prawn,
    KingPrawn
}

