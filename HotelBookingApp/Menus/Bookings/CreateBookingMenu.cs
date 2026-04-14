using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Bookings
{
    internal class CreateBookingMenu
    {
        private readonly ConsoleMenu<BookingEditorMenuOption> _menu;

        internal CreateBookingMenu()
        {
            _menu = new ConsoleMenu<BookingEditorMenuOption>(
                "--Create New Booking--", new List<MenuChoice<BookingEditorMenuOption>>());
        }
        internal BookingEditorMenuOption Show(int? customerId, int? roomId, DateOnly? startDate, DateOnly? endDate)
        {
            _menu.SetChoices(new List<MenuChoice<BookingEditorMenuOption>>
            {
                new(BookingEditorMenuOption.CustomerId, $"Customer ID: {customerId?.ToString() ?? "Not set"}"),
                new(BookingEditorMenuOption.RoomId, $"Room ID: {roomId?.ToString() ?? "Not set"}"),
                new(BookingEditorMenuOption.StartDate, $"Start Date (yyyy-MM-dd): {startDate?.ToString("yyyy-MM-dd") ?? "Not set"}"),
                new(BookingEditorMenuOption.EndDate, $"End Date (yyyy-MM-dd): {endDate?.ToString("yyyy-MM-dd")?? "Not set"}"),
                new(BookingEditorMenuOption.Save, $"Save"),
                new(BookingEditorMenuOption.Cancel, $"Cancel")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return BookingEditorMenuOption.Cancel;
            return result.Value;

        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
