using HotelBookingApp.Enums;
using HotelBookingApp.Services;
using HotelBookingApp.Utilities;

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
            int customerId = InputHelper.ReadInt("Enter customer ID: ");
            int roomId = InputHelper.ReadInt("Enter room ID: ");
            DateOnly startDate = InputHelper.ReadDateOnly("Enter start date (yyyy-MM-dd): ");
            DateOnly endDate = InputHelper.ReadDateOnly("Enter end date (yyyy-MM-dd): ");

            BookingResult result = _bookingService.CreateBooking(customerId, roomId, startDate, endDate);
            
            switch (result)
            {
                case BookingResult.Success:
                    Console.WriteLine("Booking created.");
                    break;
                case BookingResult.InvalidDateRange:
                    Console.WriteLine("End date must be after start date.");
                    break;
                case BookingResult.RoomUnavailable:
                    Console.WriteLine("Room is unavailable these dates.");
                    break;
            }
            InputHelper.WaitForBack();
        }
    }
}