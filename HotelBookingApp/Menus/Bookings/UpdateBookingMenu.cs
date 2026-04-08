using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Models;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Bookings
{
    internal class UpdateBookingMenu
    {
        private readonly ConsoleMenu<UpdateBookingMenuOption> _menu;
        internal UpdateBookingMenu()
        {
            _menu = new ConsoleMenu<UpdateBookingMenuOption>(
                "Update Booking", 
                new List<MenuChoice<UpdateBookingMenuOption>>());
        }
        internal UpdateBookingMenuOption Show(DateOnly startDate, DateOnly endDate, int roomId, int customerId)
        {
            _menu.SetChoices(new List<MenuChoice<UpdateBookingMenuOption>>
            {
                new(UpdateBookingMenuOption.StartDate, $"Change start date ({startDate:yyyy-MM-dd})"),
                new(UpdateBookingMenuOption.EndDate, $"Change end date ({endDate:yyyy-MM-dd})"),
                new(UpdateBookingMenuOption.RoomId, $"Change room by room ID ({roomId})"),
                new(UpdateBookingMenuOption.CustomerId, $"Change customer by customer ID ({customerId})"),
                new(UpdateBookingMenuOption.Save, "Save changes"),
                new(UpdateBookingMenuOption.Back, "Back")
            });
            return _menu.Show();
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
