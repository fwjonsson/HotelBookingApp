using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities.Menus;

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
                new(ReadBookingMenuOption.ReadByID, "Find booking by ID"),
                new(ReadBookingMenuOption.ReadByRoomID, "Find booking by room ID"),
                new(ReadBookingMenuOption.ReadByCustomerID, "Find booking by customer ID"),
                new(ReadBookingMenuOption.Back, "Back")
            });
        }
        internal ReadBookingMenuOption Show()
        {
            var result = _menu.Show();
            if (result.WasCancelled)
                return ReadBookingMenuOption.Back;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
