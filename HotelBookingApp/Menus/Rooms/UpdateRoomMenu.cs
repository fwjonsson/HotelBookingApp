using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Rooms
{
    internal class UpdateRoomMenu
    {
        private readonly ConsoleMenu<RoomEditorMenuOption> _menu;

        internal UpdateRoomMenu()
        {
            _menu = new ConsoleMenu<RoomEditorMenuOption>("Update Room", new List<MenuChoice<RoomEditorMenuOption>>());
        }
        internal RoomEditorMenuOption Show(int roomNumber, RoomType roomType, int extraBeds)
        {
            _menu.SetChoices(new List<MenuChoice<RoomEditorMenuOption>>
            {
                new(RoomEditorMenuOption.RoomNumber, $"Change room number ({roomNumber}"),
                new(RoomEditorMenuOption.RoomType, $"Change room type ({roomType.ToString()})"),
                new(RoomEditorMenuOption.ExtraBeds, $"Change amount of extra beds ({extraBeds})"),
                new(RoomEditorMenuOption.Save, "Save changes"),
                new(RoomEditorMenuOption.Cancel, "Cancel")
            });
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
