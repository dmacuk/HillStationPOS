﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HillStationPOS.Interfaces;
using HillStationPOS.Model;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         See http://www.mvvmlight.net
    ///     </para>
    /// </summary>
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainViewModel : ViewModelBase
    {
        public delegate void OrderItemAddedEventHandler(object sender, OrderItemAddedEventArgs e);

        private readonly IDataService _dataService;

        private readonly IWindowService _windowService;
        private Header _header;

        private List<Header> _headers;

        private Meal _meal;

        private List<Meal> _meals;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService, IWindowService windowService)
        {
            _dataService = dataService;
            _windowService = windowService;
            Headers = new List<Header>();
            Meals = new List<Meal>();
            Order = new ObservableCollection<IOrderItem>();
            if (IsInDesignMode)
            {
                var orderItem = new OrderItem
                {
                    Description = "Meal Description",
                    Price = 10.95M,
                    Notes = "These are meal notes"
                };
                Order.Add(orderItem);

                var header = new Header {Title = "Starters"};
                Headers.Add(header);

                var meal = new Meal
                {
                    Price = 5m,
                    ChickenPrice = 5m,
                    KingPrawnPrice = 5m,
                    LambPrice = 5m,
                    PrawnPrice = 5m,
                    VegetablePrice = 5m,
                    Title = "This is a meal"
                };
                Meals.Add(meal);
            }
            Order.CollectionChanged += UpdateTotals;
        }

        public ICommand AddSetMeal => new RelayCommand(() =>
        {
            IOrderItem meal = new SetMeal();
            Order.Add(meal);
        });

        public Header Header
        {
            get { return _header; }
            set
            {
                var oldMeals = Header == null ? new List<Meal>() : Header.Meals;
                if (!Set("Header", ref _header, value)) return;
                foreach (var meal in oldMeals)
                {
                    meal.MealAdded -= MealAdded;
                }

                var newMeals = value == null ? new List<Meal>() : value.Meals;
                foreach (var meal in newMeals)
                {
                    meal.MealAdded += MealAdded;
                }
                Meals = new List<Meal>(newMeals);
                if (Meals.Count > 0) Meal = Meals[0];
            }
        }

        public List<Header> Headers
        {
            get { return _headers; }
            set { Set("Headers", ref _headers, value); }
        }

        public Meal Meal
        {
            get { return _meal; }
            set { Set("Meal", ref _meal, value); }
        }

        public List<Meal> Meals
        {
            get { return _meals; }
            set { Set("Meals", ref _meals, value); }
        }

        public ObservableCollection<IOrderItem> Order { get; set; }

        public decimal OrderTotal
        {
            get { return Order.Sum(x => x.Price); }
        }

        public ICommand SetupMenu => new RelayCommand(() =>
        {
            if (_windowService.ModifyMenu())
            {
                LoadData();
            }
        });

        public event OrderItemAddedEventHandler OrderItemAdded;

        ////}

        ////    base.Cleanup();
        ////    // Clean up if needed
        ////{
        ////public override void Cleanup()
        public async void LoadData()
        {
            var headers = await _dataService.LoadMenuAsync();
            Headers = new List<Header>(headers);
            if (headers.Count > 0)
            {
                Header = headers[0];
            }
        }

        protected virtual void OnOrderItemAdded(OrderItemAddedEventArgs e)
        {
            OrderItemAdded?.Invoke(this, e);
        }

        private void DeleteOrderItem(object sender, EventArgs e)
        {
            var item = (OrderItem) sender;
            Order.Remove(item);
        }

        private void MealAdded(object sender, MealAddedEventArgs e)
        {
            var meal = sender as Meal;
            if (meal == null) return;
            var id = Order.Count == 0 ? 1 : Order.Max(order => order.Id) + 1;
            IOrderItem item = new OrderItem();
            item.AddMeal(id, meal, e.MealType);
            item.OrderDeleted += DeleteOrderItem;
            Order.Add(item);
            OnOrderItemAdded(new OrderItemAddedEventArgs {OrderItem = item});
        }

        private void UpdateTotals(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("OrderTotal");
        }
    }

    public class OrderItemAddedEventArgs : EventArgs
    {
        public IOrderItem OrderItem { get; set; }
    }
}