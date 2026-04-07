
using HotelBookingApp.Services;
using HotelBookingApp.Utilities;

namespace HotelBookingApp.Flows.Bookings
{
    internal class DeleteBookingFlow
    {
        private readonly BookingService _bookingService;

        internal DeleteBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        internal void Run()
        {
            var id = InputHelper.ReadInt("Enter booking ID: ");
            _bookingService.DeleteBooking(id);
        }
    }
}