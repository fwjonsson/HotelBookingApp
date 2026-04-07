using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services;
using HotelBookingApp.Utilities;
using HotelBookingApp.Models;

namespace HotelBookingApp.Flows.Bookings
{
    internal class UpdateBookingFlow
    {
        private readonly BookingService _bookingService;

        internal UpdateBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        internal void Run()
        {
            var id = InputHelper.ReadInt("Enter booking ID: ");
            Booking? booking = _bookingService.GetById(id);
            if (booking == null)
            {
                Console.WriteLine("No booking with that ID found.");
                BookingPrinter.WaitForBack();
                return;
            }

            DateOnly? newStartDate = booking.StartDate;
            DateOnly? newEndDate = booking.EndDate;
            int? newRoomId = booking.RoomId;
            int? newCustomerId = booking.CustomerId;

                InputHelper.ReadDateOnly("New start date (yyyy-MM-dd): ");
            
                InputHelper.ReadDateOnly("New end date (yyyy-MM-dd): ");
            
                InputHelper.ReadInt("New room ID: ");
             
                InputHelper.ReadInt("New customer ID: ");

            _bookingService.UpdateBooking(booking, newStartDate, newEndDate, newRoomId, newCustomerId);
        }
    }
}