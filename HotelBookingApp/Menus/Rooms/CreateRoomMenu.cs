using HotelBookingApp.Utilities;
using HotelBookingApp.Enums.Menus.Rooms;
using System;
using System.Collections.Generic;
using System.Text;
using HotelBookingApp.Menus.Customers;

namespace HotelBookingApp.Menus.Rooms
{
    internal class CreateRoomMenu
    {
        private readonly ConsoleMenu<EditRoomMenuOption> _menu;

        internal CreateRoomMenu()
        {
            _menu = new ConsoleMenu<EditRoomMenuOption>("Create Room", new List<MenuChoice<EditRoomMenuOption>>());
        }
        internal EditRoomMenuOption Show(int? roomNumber, int? roomType, int? extraBeds)
        {
            _menu.SetChoices(new List<MenuChoice<EditRoomMenuOption>>
            {
                new(EditRoomMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(EditRoomMenuOption.RoomType, $"Room Type(1 or 2): {roomType?.ToString() ?? "Not set"}"),
                new(EditRoomMenuOption.ExtraBeds, $"Extra Beds(1 allowed in single room and 2 in double room): {extraBeds?.ToString() ?? "Not set"}"),
                new(EditRoomMenuOption.Create, "Save"),
                new(EditRoomMenuOption.Cancel, "Cancel")
            });
        }
    }
}
