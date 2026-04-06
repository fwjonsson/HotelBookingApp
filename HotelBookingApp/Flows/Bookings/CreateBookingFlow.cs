using HotelBookingApp.Services;

namespace HotelBookingApp.Flows.Bookings
{
    internal class CreateBookingFlow
    {
        private readonly BookingService _bookingService;

        internal CreateBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        internal void Run()
        {
            _bookingService.CreateBooking();
        }
    }
}