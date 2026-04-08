
using HotelBookingApp.Enums;
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
            var bookingId = InputHelper.ReadInt("Enter booking ID: ");
            Booking? booking = _bookingService.GetById(bookingId);
            if (booking == null)
            {
                Console.WriteLine("No booking found");
                InputHelper.WaitForBack();
                return;
            }
            BookingPrinter.PrintBooking(booking);
            if (InputHelper.WaitForYesOrNo("Are you sure you want to delete? Y/N"))
            {
                BookingResult result = _bookingService.DeleteBooking(bookingId); 

                if (result == BookingResult.Success)
                {
                    Console.WriteLine("Booking deleted.");
                }
                else
                {
                    Console.WriteLine("Could not delete booking.");
                }
            }
            InputHelper.WaitForBack();
        }
    }
}