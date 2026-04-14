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
                new(RoomEditorMenuOption.RoomType, $"Room Type: \"Not set\" (1 or 2)"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: Enter Room Type First (Single room: Max 1. Double room: Max 2)"),
                new(RoomEditorMenuOption.Save, "Save"),
                new(RoomEditorMenuOption.Cancel, "Cancel")
            });
            }
            else if (roomType == RoomType.Single)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type (1 or 2): Single"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: (Max 1) {extraBeds?.ToString() ?? "Not set"}"),
                new(RoomEditorMenuOption.Save, "Save"),
                new(RoomEditorMenuOption.Cancel, "Cancel")
            });
            }
            else if (roomType == RoomType.Double)
            {
                _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Room Number: {roomNumber?.ToString() ?? "Not set" }"),
                new(RoomEditorMenuOption.RoomType, $"Room Type (1 or 2): Double"),
                new(RoomEditorMenuOption.ExtraBeds, $"Extra Beds: {extraBeds?.ToString() ?? "Not set"} (Max 2) "),
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
