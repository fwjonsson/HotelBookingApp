using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus.Rooms
{
    internal class AvailableRoomsMenu
    {
        private readonly ConsoleMenu<RoomMenuOption> _menu;
        internal AvailableRoomsMenu()
        {
            _menu = new ConsoleMenu<RoomMenuOption>("--Room Menu--", new List<MenuChoice<RoomMenuOption>>
            {
                new(RoomMenuOption.ShowAll, "Show All Available Rooms"),
                new(RoomMenuOption.FindByRoomNR, "Find By Room Number"),
                new(RoomMenuOption.FindByRoomType, "Find Available Rooms By Size (1 or 2)"),
                new(RoomMenuOption.ChangeExtraBeds, "Change Extra Beds")
            });
        }
        internal RoomMenuOption Show()
        {
            var result = _menu.Show();
            if (result.WasCancelled)
                return RoomMenuOption.Back;
            return result.Value;
        }
        internal void ResetMenu() => _menu.ResetMenu();
    }
}
