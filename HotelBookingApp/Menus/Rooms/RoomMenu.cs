using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Rooms
{
    internal class RoomMenu
    {
        private readonly ConsoleMenu<RoomMenuOption> _menu;

        internal RoomMenu()
        {
            _menu = new ConsoleMenu<RoomMenuOption>("--Room Menu--", new List<MenuChoice<RoomMenuOption>>
            {
                new(RoomMenuOption.Create, "Create New Room"),
                new(RoomMenuOption.ShowAll, "Show All Rooms"),
                new(RoomMenuOption.FindByRoomNR, "Find By Room Number"),
                new(RoomMenuOption.FindByRoomType, "Find By Size (1 or 2)"),
                new(RoomMenuOption.FindByID, "Find By Room ID"),
                new(RoomMenuOption.FindByCustomerID, "Find By Customer ID"),
                new(RoomMenuOption.FindByBookingID, "Find By Booking ID")
            });
        }
        internal RoomMenuOption Show()
        {
            var result = _menu.Show();
            if (result.WasCancelled)
                return RoomMenuOption.Back;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
