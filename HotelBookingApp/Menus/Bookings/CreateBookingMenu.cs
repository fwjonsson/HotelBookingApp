using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Bookings
{
    internal class CreateBookingMenu
    {
        private readonly ConsoleMenu<CreateBookingMenuOption> _menu;

        internal CreateBookingMenu()
        {
            _menu = new ConsoleMenu<CreateBookingMenuOption>(
                "Create Booking", new List<MenuChoice<CreateBookingMenuOption>>());
        }
        internal CreateBookingMenuOption Show(int? customerId, int? roomId, DateOnly? startDate, DateOnly? endDate)
        {
            _menu.SetChoices(new List<MenuChoice<CreateBookingMenuOption>>
            {
                new(CreateBookingMenuOption.CustomerId, $"Customer ID: {customerId?.ToString() ?? "Not set"}"),
                new(CreateBookingMenuOption.RoomId, $"Room ID: {roomId?.ToString() ?? "Not set"}"),
                new(CreateBookingMenuOption.StartDate, $"Start date (yyyy-MM-dd): {startDate?.ToString("yyyy-MM-dd") ?? "Not set"}"),
                new(CreateBookingMenuOption.EndDate, $"End date (yyyy-MM-dd): {endDate?.ToString("yyyy-MM-dd")?? "Not set"}"),
                new(CreateBookingMenuOption.Create, $"Create booking"),
                new(CreateBookingMenuOption.Cancel, $"Cancel")
            });
            return _menu.Show();
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
