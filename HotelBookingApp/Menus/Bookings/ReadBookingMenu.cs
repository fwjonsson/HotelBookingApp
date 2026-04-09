using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities;

namespace HotelBookingApp.Menus.Bookings
{
    internal class ReadBookingMenu
    {
        private readonly ConsoleMenu<ReadBookingMenuOption> _menu;

        internal ReadBookingMenu()
        {
            _menu = new ConsoleMenu<ReadBookingMenuOption>("Read Booking Menu", new List<MenuChoice<ReadBookingMenuOption>>
            {
                new(ReadBookingMenuOption.ReadAll, "Show all bookings"),
                new(ReadBookingMenuOption.ReadByDate, "Find booking by date"),
                new(ReadBookingMenuOption.ReadById, "Find booking by ID"),
                new(ReadBookingMenuOption.ReadByRoomId, "Find booking by room ID"),
                new(ReadBookingMenuOption.ReadByCustomerId, "Find booking by customer ID"),
                new(ReadBookingMenuOption.Back, "Back")
            });
        }
        internal ReadBookingMenuOption Show()
        {
            return _menu.Show();
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
