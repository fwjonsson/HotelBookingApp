using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus.Rooms
{

    internal class RoomActionMenu<T>
    {
        private readonly ConsoleMenu<RoomActionMenuOption> _menu;
        private readonly string _item;
        private readonly Func<T, string> _formatter;

        internal RoomActionMenu(string item, Func<T, string> formatter)
        {
            _item = item;
            _formatter = formatter;
            _menu = new ConsoleMenu<RoomActionMenuOption>("", new List<MenuChoice<RoomActionMenuOption>>());
        }
        internal RoomActionMenuOption Show(T type)
        {
            _menu.SetTitle($"{_item}\n\n{_formatter(type)}");
            _menu.SetChoices(new List<MenuChoice<RoomActionMenuOption>>
                {
                new(RoomActionMenuOption.ShowBookings, "Show Bookings On Room"),
                new(RoomActionMenuOption.Edit, "Edit"),
                new(RoomActionMenuOption.Delete, "Delete")
                });
            var result = _menu.Show();
            if (result.WasCancelled)
                return RoomActionMenuOption.Back;

            return result.Value;
        }
        internal void ResetMenu() => _menu.ResetMenu();
    }

}
