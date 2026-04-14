using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Models;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Bookings
{
    internal class UpdateBookingMenu
    {
        private readonly ConsoleMenu<BookingEditorMenuOption> _menu;
        internal UpdateBookingMenu()
        {
            _menu = new ConsoleMenu<BookingEditorMenuOption>(
                "Update Booking"
                , new List<MenuChoice<BookingEditorMenuOption>>());
        }
        internal BookingEditorMenuOption Show(DateOnly startDate, DateOnly endDate, int roomId, int customerId)
        {
            _menu.SetChoices(new List<MenuChoice<BookingEditorMenuOption>>
            {
                new(BookingEditorMenuOption.StartDate, $"Start date ({startDate:yyyy-MM-dd})"),
                new(BookingEditorMenuOption.EndDate, $"End date ({endDate:yyyy-MM-dd})"),
                new(BookingEditorMenuOption.RoomId, $"Room ID ({roomId})"),
                new(BookingEditorMenuOption.CustomerId, $"Change customer by customer ID ({customerId})"),
                new(BookingEditorMenuOption.Save, "Save changes"),
                new(BookingEditorMenuOption.Cancel, "Cancel")
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
