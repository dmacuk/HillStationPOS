//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HillStationPOS.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Meal
    {
        public long Id { get; set; }
        public long HeaderId { get; set; }
        public Nullable<short> MealNumber { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal ChickenPrice { get; set; }
        public decimal LambPrice { get; set; }
        public decimal VegetablePrice { get; set; }
        public decimal PrawnPrice { get; set; }
        public decimal KingPrawnPrice { get; set; }
        public short DisplayOrder { get; set; }
    
        public virtual Header Header { get; set; }
    }
}
