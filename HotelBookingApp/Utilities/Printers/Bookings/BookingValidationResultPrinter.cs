using HotelBookingApp.Services.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities.Printers.Bookings
{
    internal static class BookingValidationResultPrinter
    {
        internal static void PrintErrorsOrSuccess(BookingValidationResult result, string messageOnSuccess)
        {
            if(result.Success)
            {
                Console.WriteLine(messageOnSuccess);
                return;
            }
            if (!result.BookingFound)
            {
                Console.WriteLine("Booking not found.");
                return;
            }
            if(!result.ValidDateRange)
                Console.WriteLine("Invalid date range.");

            if(!result.CustomerFound)
                Console.WriteLine("Customer not found.");

            if(!result.RoomFound)
                Console.WriteLine("Room not found.");

            else if (!result.RoomAvailable)
                Console.WriteLine("Room is not available those dates.");
        }
    }
}
