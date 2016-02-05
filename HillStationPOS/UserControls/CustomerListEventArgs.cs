using System;
using HillStationPOS.Model.Entities;

namespace HillStationPOS.UserControls
{
    public class CustomerListEventArgs : EventArgs
    {
        public Customer Customer { get; set; }
    }
}