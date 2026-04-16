using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Bookings
{
    internal class BookingActionMenu<T>
    {
        private readonly ConsoleMenu<BookingActionMenuOption> _menu;
        private readonly string _item;
        private readonly Func<T, string> _formatter;

        internal BookingActionMenu(string item, Func<T, string> formatter)
        {
            _item = item;
            _formatter = formatter;
            _menu = new ConsoleMenu<BookingActionMenuOption>("", new List<MenuChoice<BookingActionMenuOption>>());
        }
        internal BookingActionMenuOption Show(T type)
        {
            _menu.SetTitle($"{_item}\n\n{_formatter(type)}");
            _menu.SetChoices(new List<MenuChoice<BookingActionMenuOption>>
            {
                new(BookingActionMenuOption.ShowCustomer, "Show Customer Details"),
                new(BookingActionMenuOption.ShowRoom, "Show Room Details"),
                new(BookingActionMenuOption.Edit, "Edit"),
                new(BookingActionMenuOption.Delete, "Delete")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return BookingActionMenuOption.Back;

            return result.Value;
        }
        internal void ResetMenu() => _menu.ResetMenu();
    }

}
