using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
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
        internal void Run(Booking booking)
        {
            BookingPrinter.PrintBooking(booking);
            if (InputHelper.WaitForYesOrNo("Are you sure you want to delete? Y/N"))
            {
                BookingResult result = _bookingService.DeleteBooking(booking.Id);

                switch (result)
                {
                    case BookingResult.Success:
                        Console.WriteLine("Booking deleted.");
                        break;
                    case BookingResult.BookingNotFound:
                        Console.WriteLine("No booking with that ID found.");
                        break;
                    default:
                        Console.WriteLine("Could not delete booking.");
                        break;
                }
            }
        }
    }
}