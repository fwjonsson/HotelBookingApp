using HotelBookingApp.Services;

namespace HotelBookingApp.Flows.Bookings
{
    internal class UpdateBookingFlow
    {
        private readonly BookingService _bookingService;


        internal UpdateBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
    }
}