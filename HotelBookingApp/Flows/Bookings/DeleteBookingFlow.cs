
using HotelBookingApp.Models;
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
            Booking? booking = _bookingService.GetById(id);
            if (booking == null)
            {
                Console.WriteLine("No booking found");
                return;
            }
            if (InputHelper.WaitForYesOrNo("Are you sure you want to delete?"))
            {
                _bookingService.DeleteBooking();
            }
            return;
        }
    }
}