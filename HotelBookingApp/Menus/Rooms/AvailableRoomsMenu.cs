using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Models;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus.Rooms
{
    internal class AvailableRoomsMenu
    {
        private readonly ConsoleMenu<RoomMenuOption> _menu;
        internal AvailableRoomsMenu()
        {
            _menu = new ConsoleMenu<RoomMenuOption>("--Room Menu--",
                new List<MenuChoice<RoomMenuOption>> { });
        }
        internal RoomMenuOption Show(Room? room, int? newExtraBeds)
        {
            _menu.SetTitle(room is null ? "Select Room" : $"Selected Room Nr: {room.RoomNumber}");
            _menu.SetChoices
                (
                new List<MenuChoice<RoomMenuOption>>{
                new(RoomMenuOption.ShowAll, "Show All Available Rooms"),
                new(RoomMenuOption.FindByRoomNr, "Find By Room Nr"),
                new(RoomMenuOption.FindByRoomType, "Find Available Rooms By Size (1 or 2)"),
                new(RoomMenuOption.ChangeExtraBeds, room is null ? "Extra Beds: (Must Select Room First)" : $"Extra Beds: {newExtraBeds}"), 
                new(RoomMenuOption.Save, "Save")
                });
            var result = _menu.Show();
            if (result.WasCancelled)
                return RoomMenuOption.Back;
            return result.Value;
        }
        internal void ResetMenu() => _menu.ResetMenu();
    }
}
