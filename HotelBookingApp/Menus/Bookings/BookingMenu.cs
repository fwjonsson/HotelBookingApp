using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities;

namespace HotelBookingApp.Menus.Bookings
{
    internal class BookingMenu
    {
        private readonly ConsoleMenu<BookingMenuOption> _menu;

        internal BookingMenu()
        {
            _menu = new ConsoleMenu<BookingMenuOption>("Booking Menu", new List<MenuChoice<BookingMenuOption>>
            {
                new(BookingMenuOption.Create, "Create Booking"),
                new(BookingMenuOption.Read, "Read Booking"),
                new(BookingMenuOption.Update, "Update Booking"),
                new(BookingMenuOption.Delete, "Delete Booking"),
                new(BookingMenuOption.Back, "Back")
            });
        }
        internal BookingMenuOption Show()
        {
            return _menu.Show();
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
