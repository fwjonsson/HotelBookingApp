using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Utilities.Menus;

namespace HotelBookingApp.Menus.Bookings
{
    internal class BookingMenu
    {
        private readonly ConsoleMenu<BookingMenuOption> _menu;

        internal BookingMenu()
        {
            _menu = new ConsoleMenu<BookingMenuOption>("--Booking Menu--", new List<MenuChoice<BookingMenuOption>>
            {
                new(BookingMenuOption.Create, "Create New Booking"),
                new(BookingMenuOption.ShowAll, "Show All Bookings"),
                new(BookingMenuOption.FindByDate, "Find By Date"),
                new(BookingMenuOption.FindById, "Find By Booking ID"),
                new(BookingMenuOption.FindByRoomId, "Find By Room ID"),
                new(BookingMenuOption.FindByCustomerId, "Find By Customer ID")
            });
        }
        internal BookingMenuOption Show()
        {
            var result = _menu.Show();
            if (result.WasCancelled)
                return BookingMenuOption.Back;
            return result.Value;
        }
        internal void ResetMenu() => _menu.ResetMenu();
    }
}
