using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Utilities.Menus;

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
                new(RoomMenuOption.FindByRoomNr, "Find By Room Number"),
                new(RoomMenuOption.FindByRoomType, "Find By Size (1 or 2)"),
                new(RoomMenuOption.FindById, "Find By Room ID"),
                new(RoomMenuOption.FindByCustomerId, "Find By Customer ID"),
                new(RoomMenuOption.FindByBookingId, "Find By Booking ID")
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
