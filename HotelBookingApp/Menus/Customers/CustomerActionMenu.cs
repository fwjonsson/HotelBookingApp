using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{

    internal class CustomerActionMenu<T>
    {
        private readonly ConsoleMenu<CustomerActionMenuOption> _menu;
        private readonly string _item;
        private readonly Func<T, string> _formatter;

        internal CustomerActionMenu(string item, Func<T, string> formatter)
        {
            _item = item;
            _formatter = formatter;
            _menu = new ConsoleMenu<CustomerActionMenuOption>("", new List<MenuChoice<CustomerActionMenuOption>>());
        }
        internal CustomerActionMenuOption Show(T type)
        {
            _menu.SetTitle($"{_item}\n\n{_formatter(type)}");
            _menu.SetChoices(new List<MenuChoice<CustomerActionMenuOption>>
            {
                new(CustomerActionMenuOption.ShowBookings, "Show Bookings By Customer."),
                new(CustomerActionMenuOption.Edit, "Edit"),
                new(CustomerActionMenuOption.Delete, "Delete")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return CustomerActionMenuOption.Back;

            return result.Value;
        }
        internal void ResetMenu() => _menu.ResetMenu();
    }

}
