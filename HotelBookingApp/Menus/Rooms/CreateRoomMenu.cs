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
        private readonly ConsoleMenu<RoomEditorMenuOption> _menu;

        internal CreateRoomMenu()
        {
            _menu = new ConsoleMenu<RoomEditorMenuOption>("Create Room", new List<MenuChoice<RoomEditorMenuOption>>());
        }
        internal RoomEditorMenuOption Show(int? roomNumber, RoomType? roomType, int? extraBeds)
        {
            if (roomType == null)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type(1 or 2): {roomType?.ToString() ?? "Not set"}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds(Max 1 in single room and max 2 in double room): Enter Room Type First"),
                new(RoomEditorMenuOption.Save, "Save"),
                new(RoomEditorMenuOption.Cancel, "Cancel")
            });
            }
            else
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type(1 or 2): {roomType?.ToString() ?? "Not set"}"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds(Max 1 in single room and max 2 in double room): {extraBeds?.ToString() ?? "Not set"}"),
                new(RoomEditorMenuOption.Save, "Save"),
                new(RoomEditorMenuOption.Cancel, "Cancel")
            });

            }
            var result = _menu.Show();
            if (result.WasCancelled)
                return RoomEditorMenuOption.Cancel;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
