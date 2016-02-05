using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.ViewModel
{
    public enum UpdateOperation
    {
        Updated,
        Cancelled
    }

    public class MenuUpdatedEventArgs : EventArgs
    {
        public UpdateOperation UpdateOperation { get; set; }
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class MenuViewModel : ViewModelBase
    {
        public delegate void MenuUdateEventHandler(object sender, MenuUpdatedEventArgs e);

        private Header _header;

        private List<Meal> _meals;

        public MenuViewModel()
        {
            Headers = new List<Header>();
            if (IsInDesignMode)
            {
                var header = new Header {Title = "Header"};
                var meal = new Meal {Title = "Meal"};
                header.Meals.Add(meal);
                Headers.Add(header);
                Meals = new List<Meal>(header.Meals);
                Header = header;
            }
            else
            {
                using (var entities = new HillStationEntities())
                {
                    foreach (var header in entities.Headers.Include(h => h.Meals).OrderBy(h => h.DisplayOrder).ToList())
                    {
                        Headers.Add(new Header(header));
                    }
                }
            }

            Cancel = new RelayCommand(CancelUpdate);
            SaveData = new RelayCommand(Save);
            Decrement = new RelayCommand<string>(p => AdjustPrice(p, -0.05m));
            Increment = new RelayCommand<string>(p => AdjustPrice(p, 0.05m));
        }

        public Header Header
        {
            get { return _header; }
            set
            {
                if (Set("Header", ref _header, value))
                {
                    Meals = new List<Meal>(value.Meals);
                }
            }
        }

        public List<Header> Headers { get; set; }

        public List<Meal> Meals
        {
            get { return _meals; }
            set { Set("Meals", ref _meals, value); }
        }

        public event MenuUdateEventHandler MenuUpdated;

        protected virtual void OnMenuOpdated(MenuUpdatedEventArgs e)
        {
            MenuUpdated?.Invoke(this, e);
        }

        private void AdjustPrice(string priceType, decimal amount)
        {
            foreach (var meal in Header.Meals)
            {
                MakeAdjustment(meal, priceType, amount);
            }
        }

        private void CancelUpdate()
        {
            OnMenuOpdated(new MenuUpdatedEventArgs {UpdateOperation = UpdateOperation.Cancelled});
        }

        private void MakeAdjustment(Meal meal, string priceType, decimal amount)
        {
            switch (priceType)
            {
                case "Price":
                    if (meal.Price == 0 && amount < 0) return;
                    meal.Price = meal.Price + amount;
                    break;

                case "Chicken":
                    if (meal.ChickenPrice == 0 && amount < 0) return;
                    meal.ChickenPrice = meal.ChickenPrice + amount;
                    break;

                case "Lamb":
                    if (meal.LambPrice == 0 && amount < 0) return;
                    meal.LambPrice = meal.LambPrice + amount;
                    break;

                case "Vegetable":
                    if (meal.VegetablePrice == 0 && amount < 0) return;
                    meal.VegetablePrice = meal.VegetablePrice + amount;
                    break;

                case "Prawn":
                    if (meal.PrawnPrice == 0 && amount < 0) return;
                    meal.PrawnPrice = meal.PrawnPrice + amount;
                    break;

                case "KingPrawn":
                    if (meal.KingPrawnPrice == 0 && amount < 0) return;
                    meal.KingPrawnPrice = meal.KingPrawnPrice + amount;
                    break;
            }
        }

        private void Save()
        {
            using (var entities = new HillStationEntities())
            {
                foreach (var header in Headers)
                {
                    entities.Headers.Attach(header);
                    entities.Entry(header).State = EntityState.Modified;
                    foreach (var meal in header.Meals)
                    {
                        entities.Meals.Attach(meal);
                        entities.Entry(meal).State = EntityState.Modified;
                    }
                }
                entities.SaveChanges();
                OnMenuOpdated(new MenuUpdatedEventArgs {UpdateOperation = UpdateOperation.Updated});
            }
        }

        #region Commands

        public ICommand Cancel { get; set; }
        public ICommand Decrement { get; set; }
        public ICommand Increment { get; set; }
        public ICommand SaveData { get; set; }

        #endregion Commands
    }
}