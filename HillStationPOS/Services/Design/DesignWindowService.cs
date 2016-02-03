using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using HillStationPOS.Interfaces;

namespace HillStationPOS.Services.Design
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class DesignWindowService : IWindowService
    {
        public bool ModifyMenu()
        {
            throw new NotImplementedException();
        }

        public bool GetPassword(Window owner)
        {
            throw new NotImplementedException();
        }

        public void ShowUtilitiesMenu(Window window)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(Window owner)
        {
            throw new NotImplementedException();
        }
    }
}