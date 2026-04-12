using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{
    internal class CustomerMenu
    {
        private readonly ConsoleMenu<CustomerMenuOption> _menu;

        internal CustomerMenu()
        {
            _menu = new ConsoleMenu<CustomerMenuOption>("Customer menu", new List<MenuChoice<CustomerMenuOption>>
            {
                new(CustomerMenuOption.Create, "Create New Customer"),
                new(CustomerMenuOption.Read, "Read Customers"),
                new(CustomerMenuOption.Update, "Update Customer"),
                new(CustomerMenuOption.Delete, "Delete Customer")
            });
        }
        internal CustomerMenuOption Show()
        {
            var result = _menu.Show();
            if (result.WasCancelled)
                return CustomerMenuOption.Delete;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
