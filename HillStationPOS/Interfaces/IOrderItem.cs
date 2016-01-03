using System.Windows.Input;
using HillStationPOS.Model;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.Interfaces
{
    public interface IOrderItem
    {
        short Quantity { get; set; }
        decimal Price { get; set; }
        string Description { get; set; }
        string Notes { get; set; }
        ICommand Delete { get; }
        int Id { get; set; }
        event AbstractOrderItem.OrderDeletedEventHandler OrderDeleted;
        void AddMeal(int id, Meal meal, MealType mealType);
    }
}