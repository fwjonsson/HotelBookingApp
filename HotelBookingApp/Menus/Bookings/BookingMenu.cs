using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus.Bookings
{
    internal class BookingMenu
    {
        private readonly ConsoleMenu<BookingMenuOption> _menu;

        internal BookingMenu()
        {
            _menu = new ConsoleMenu<BookingMenuOption>("Booking Menu", new List<MenuChoice<BookingMenuOption>>
            {
                new(BookingMenuOption.Create, "Create New Booking"),
                new(BookingMenuOption.ShowAll, "Read Bookings"),
                new(BookingMenuOption.FindById, "Find By ID"),
                new(BookingMenuOption.FindByDate, "Find By Date"),
                new(BookingMenuOption.FindByRoomId, "Room ID"),
                new(BookingMenuOption.FindByCustomerId, "Customer ID"),
                new(BookingMenuOption.Back, "Back")
            });
        }
        internal BookingMenuOption Show()
        {
            var result = _menu.Show();
            if (result.WasCancelled)
                return BookingMenuOption.Back;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
