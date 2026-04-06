using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services;

namespace HotelBookingApp.Flows.Bookings
{
    internal class ReadBookingFlow
    {
        private readonly BookingService _bookingService;
        private readonly ReadBookingMenu _readByBookingMenu;

        internal ReadBookingFlow(BookingService bookingService)
        {
            _readByBookingMenu = new ReadBookingMenu();

            _bookingService = bookingService;
        }
        internal void Run()
        {
            
        }
    }
}