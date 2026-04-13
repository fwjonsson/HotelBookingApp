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
            _menu = new ConsoleMenu<CustomerMenuOption>("Customer Menu", new List<MenuChoice<CustomerMenuOption>>
            {
                new(CustomerMenuOption.Create, "Create New Customer"),
                new(CustomerMenuOption.ShowAll, "Show All Customers"),
                new(CustomerMenuOption.FindByID, "Find By ID"),
                new(CustomerMenuOption.FindByFullName, "Find By Full Name"),
                new(CustomerMenuOption.FindByBirthDate, "Find By Birth Date"),
                new(CustomerMenuOption.FindByEmail, "Find By Email"),
                new(CustomerMenuOption.FindByPhoneNumber, "Find By Phone Number"),
                new(CustomerMenuOption.FindByBookingID, "Find By Booking ID"),
                new(CustomerMenuOption.Back, "Back")
            });
        }
        internal CustomerMenuOption Show()
        {
            var result = _menu.Show();
            if (result.WasCancelled)
                return CustomerMenuOption.Back;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
