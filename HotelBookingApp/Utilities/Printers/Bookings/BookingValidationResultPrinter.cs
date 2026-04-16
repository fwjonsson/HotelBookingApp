using HotelBookingApp.Services.Bookings;

namespace HotelBookingApp.Utilities.Printers.Bookings
{
    internal static class BookingValidationResultPrinter
    {
        internal static void PrintErrorsOrSuccess(BookingValidationResult result, string messageOnSuccess)
        {
            if (result.Success)
            {
                Console.WriteLine(messageOnSuccess);
                return;
            }
            if (!result.BookingFound)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            if (!result.EndDateIsAfterStartDate)
                Console.WriteLine("End date must be after start date.");

            if (!result.ValidDateRange)
                Console.WriteLine("Invalid date input.");

            if (!result.CustomerFound)
                Console.WriteLine("Customer not found.");

            if (!result.RoomFound)
                Console.WriteLine("Room not found.");

            else if (!result.RoomAvailable)
                Console.WriteLine("Room is not available those dates.");
        }
    }
}
