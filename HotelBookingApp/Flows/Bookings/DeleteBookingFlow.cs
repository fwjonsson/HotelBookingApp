
using HotelBookingApp.Services;

namespace HotelBookingApp.Flows.Bookings
{
    internal class DeleteBookingFlow
    {
        private readonly BookingService _bookingService;

        internal DeleteBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
    }
}