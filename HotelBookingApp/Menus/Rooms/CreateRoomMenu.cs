using HotelBookingApp.Enums.Menus.Rooms;
using System;
using System.Collections.Generic;
using System.Text;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Utilities.Menus;
using HotelBookingApp.Enums;

namespace HotelBookingApp.Menus.Rooms
{
    internal class CreateRoomMenu
    {
        private readonly ConsoleMenu<EditRoomMenuOption> _menu;

        internal CreateRoomMenu()
        {
            _menu = new ConsoleMenu<EditRoomMenuOption>("Create Room", new List<MenuChoice<EditRoomMenuOption>>());
        }
        internal EditRoomMenuOption Show(int? roomNumber, RoomType? roomType, int? extraBeds)
        {
            if (roomType == null)
            {
                _menu.SetChoices(new List<MenuChoice<EditRoomMenuOption>>
            {
                new(EditRoomMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(EditRoomMenuOption.RoomType, $"Room Type(1 or 2): {roomType?.ToString() ?? "Not set"}"),
                new(EditRoomMenuOption.ExtraBeds, $"Enter Room Type First"),
                new(EditRoomMenuOption.Save, "Save"),
                new(EditRoomMenuOption.Cancel, "Cancel")
            });
            }
            else
            {
                {
                    _menu.SetChoices(new List<MenuChoice<EditRoomMenuOption>>
            {
                new(EditRoomMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(EditRoomMenuOption.RoomType, $"Room Type(1 or 2): {roomType?.ToString() ?? "Not set"}"),
                new(EditRoomMenuOption.ExtraBeds, $"Extra Beds(Max 1 in single room and max 2 in double room): {extraBeds?.ToString() ?? "Not set"}"),
                new(EditRoomMenuOption.Save, "Save"),
                new(EditRoomMenuOption.Cancel, "Cancel")
            });
                }
            }
            var result = _menu.Show();
            if (result.WasCancelled)
                return EditRoomMenuOption.Cancel;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
