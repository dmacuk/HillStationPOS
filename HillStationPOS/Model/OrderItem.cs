using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Model
{
    public class OrderItem : ObservableObject
    {
        public delegate void OrderDeletedEventHandler(object sender, EventArgs e);

        private const string DescriptionPropertyName = "Description";

        private const string IdPropertyName = "Id";

        private const string NotesPropertyName = "Notes";

        private const string PricePropertyName = "Price";

        private readonly List<MealDetails> _meals = new List<MealDetails>();

        private string _description;

        private int _id;

        private decimal _myProperty;

        private string _notes;

        public ICommand Delete => new RelayCommand(() => { OnOrderDeleted(null); });

        public string Description
        {
            get { return _description; }
            set { Set(DescriptionPropertyName, ref _description, value); }
        }

        public int Id
        {
            get { return _id; }
            private set { Set(IdPropertyName, ref _id, value); }
        }

        public string Notes
        {
            get { return _notes; }
            set { Set(NotesPropertyName, ref _notes, value); }
        }

        public decimal Price
        {
            get { return _myProperty; }
            set { Set(PricePropertyName, ref _myProperty, value); }
        }

        public event OrderDeletedEventHandler OrderDeleted;

        public void AddMeal(int id, Meal meal, MealType mealType)
        {
            _meals.Add(new MealDetails(meal, mealType));
            Price = _meals.Sum(m => m.GetPrice());
            Description = GetDescription();
            Id = id;
        }

        private string GetDescription()
        {
            var result = new StringBuilder();
            foreach (var mealDetail in _meals)
            {
                result.Append(mealDetail.GetDescription()).Append(Environment.NewLine);
            }
            var length = result.Length;
            if (length > 0) result.Length = length - 1;
            return result.ToString();
        }

        private void OnOrderDeleted(EventArgs e)
        {
            OrderDeleted?.Invoke(this, e);
        }
    }

    internal class MealDetails
    {
        private readonly Meal _meal;
        private readonly MealType _mealType;

        public MealDetails(Meal meal, MealType mealType)
        {
            _meal = meal;
            _mealType = mealType;
        }

        public string GetDescription()
        {
            return _meal.GetDescripion(_mealType);
        }

        public decimal GetPrice()
        {
            return _meal.GetPrice(_mealType);
        }
    }
}