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
        private readonly ConsoleMenu<EditBookingMenuOption> _menu;
        internal UpdateBookingMenu()
        {
            _menu = new ConsoleMenu<EditBookingMenuOption>(
                "Update Booking"
                , new List<MenuChoice<EditBookingMenuOption>>());
        }
        internal EditBookingMenuOption Show(DateOnly startDate, DateOnly endDate, int roomId, int customerId)
        {
            _menu.SetChoices(new List<MenuChoice<EditBookingMenuOption>>
            {
                new(EditBookingMenuOption.StartDate, $"Change start date ({startDate:yyyy-MM-dd})"),
                new(EditBookingMenuOption.EndDate, $"Change end date ({endDate:yyyy-MM-dd})"),
                new(EditBookingMenuOption.RoomId, $"Change room by room ID ({roomId})"),
                new(EditBookingMenuOption.CustomerId, $"Change customer by customer ID ({customerId})"),
                new(EditBookingMenuOption.Save, "Save changes"),
                new(EditBookingMenuOption.Cancel, "Cancel")
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
