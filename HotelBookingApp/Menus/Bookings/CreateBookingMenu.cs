using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Bookings
{
    internal class CreateBookingMenu
    {
        private readonly ConsoleMenu<EditBookingMenuOption> _menu;

        internal CreateBookingMenu()
        {
            _menu = new ConsoleMenu<EditBookingMenuOption>(
                "Create Booking", new List<MenuChoice<EditBookingMenuOption>>());
        }
        internal EditBookingMenuOption Show(int? customerId, int? roomId, DateOnly? startDate, DateOnly? endDate)
        {
            _menu.SetChoices(new List<MenuChoice<EditBookingMenuOption>>
            {
                new(EditBookingMenuOption.CustomerId, $"Customer ID: {customerId?.ToString() ?? "Not set"}"),
                new(EditBookingMenuOption.RoomId, $"Room ID: {roomId?.ToString() ?? "Not set"}"),
                new(EditBookingMenuOption.StartDate, $"Start date (yyyy-MM-dd): {startDate?.ToString("yyyy-MM-dd") ?? "Not set"}"),
                new(EditBookingMenuOption.EndDate, $"End date (yyyy-MM-dd): {endDate?.ToString("yyyy-MM-dd")?? "Not set"}"),
                new(EditBookingMenuOption.Save, $"Create booking"),
                new(EditBookingMenuOption.Cancel, $"Cancel")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return EditBookingMenuOption.Cancel;
            return result.Value;

        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
