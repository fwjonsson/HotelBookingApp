using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Bookings
{
    internal class UpdateBookingMenu
    {
        internal UpdateBookingMenuOption Show(DateOnly startDate, DateOnly endDate, int customerId, int roomId)
        {
            var menu = new ConsoleMenu<UpdateBookingMenuOption>(
                "Update Booking", 
                new List<MenuChoice<UpdateBookingMenuOption>>
            {
                new(UpdateBookingMenuOption.StartDate, $"Change start date ({startDate:yyyy-MM-dd})"),
                new(UpdateBookingMenuOption.EndDate, $"Change end date ({endDate:yyyy-MM-dd})"),
                new(UpdateBookingMenuOption.CustomerId, $"Change customer by customer ID ({customerId})"),
                new(UpdateBookingMenuOption.RoomId, $"Change room by room ID ({roomId})"),
                new(UpdateBookingMenuOption.Save, "Save changes"),
                new(UpdateBookingMenuOption.Back, "Back")
            });
            return menu.Show();
        }
    }
}
