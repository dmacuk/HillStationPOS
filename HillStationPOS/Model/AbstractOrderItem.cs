using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HillStationPOS.Interfaces;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Model
{
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public class AbstractOrderItem : ObservableObject, IOrderItem
    {
        public delegate void OrderDeletedEventHandler(object sender, EventArgs e);

        private readonly List<MealDetails> _meals = new List<MealDetails>();

        private string _description;

        protected string _notes;
        protected decimal _price;

        public int Id { get; set; }

        public short Quantity { get; set; }

        public virtual decimal Price
        {
            get { return _price; }
            set { Set("Price", ref _price, value); }
        }

        public string Description
        {
            get { return _description; }
            set { Set("Description", ref _description, value); }
        }


        public virtual string Notes
        {
            get { return _notes; }
            set
            {
                Debug.WriteLine("Setting notes 2");
                Set("Notes", ref _notes, value);
            }
        }

        public ICommand Delete => new RelayCommand(() => { OnOrderDeleted(null); });

        public event OrderDeletedEventHandler OrderDeleted;

        public void AddMeal(int id, Meal meal, MealType mealType)
        {
            _meals.Add(new MealDetails(id, meal, mealType));
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

        protected virtual void OnOrderDeleted(EventArgs e)
        {
            OrderDeleted?.Invoke(this, e);
        }

        protected bool Equals(OrderItem other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OrderItem) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    internal class MealDetails
    {
        private readonly int _id;
        private readonly Meal _meal;
        private readonly MealType _mealType;

        public MealDetails(int id, Meal meal, MealType mealType)
        {
            _id = id;
            _meal = meal;
            _mealType = mealType;
        }

        public decimal GetPrice()
        {
            return _meal.GetPrice(_mealType);
        }

        public string GetDescription()
        {
            return _meal.GetDescripion(_mealType);
        }
    }
}