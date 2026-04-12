using HotelBookingApp.Models;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Bookings
{
    internal class SelectBookingMenu
    {
        private readonly ConsoleMenu<int> _menu;

        internal SelectBookingMenu()
        {
            _menu = new ConsoleMenu<int>("select Booking", new List<MenuChoice<int>>());
        }
        internal Booking? Show(List<Booking> bookings)
        {
            if (bookings.Count == 0)
                return null;

            var choices = new List<MenuChoice<int>>();

            for (int i = 0; i< bookings.Count; i++)
            {
                Booking booking = bookings[i];

                string text = $"{booking.Id} - {booking.StartDate} - {booking.EndDate} - {booking.CustomerId} - {booking.RoomId}";
                choices.Add(new MenuChoice<int>(i, text));
            }
            int backIndex = choices.Count;
            choices.Add(new MenuChoice<int>(backIndex, "Back"));

            _menu.SetChoices(choices);

            var selectedIndex = _menu.Show();

            if (selectedIndex == backIndex)
            {
                _menu.ResetIndex();
                return null;
            }

            return bookings[selectedIndex];

        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
