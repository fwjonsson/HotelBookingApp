using HotelBookingApp.Models;

namespace HotelBookingApp.Utilities.Printers.Bookings
{
    internal static class BookingPrinter
    {
        internal static string FormatBooking(Booking booking)
        {
            return $"Booking ID: {booking.Id}\n" +
                $"Start Date: {booking.StartDate}\n" +
                $"End Date: {booking.EndDate}\n" +
                $"Customer ID: {booking.CustomerId}\n" +
                $"Customer Name: {booking.Customer.FullName}\n" +
                $"Room Nr: {booking.Room.RoomNumber}\n";

        }
        internal static string MenuFormatBooking(Booking booking)
        {
            return $"Booking ID: {booking.Id}\n" +
                $"  Start Date: {booking.StartDate}\n" +
                $"  End Date: {booking.EndDate}\n" +
                $"  Customer ID: {booking.CustomerId}\n" +
                $"Customer Name: {booking.Customer.FullName}\n" +
                $"  Room Nr: {booking.Room.RoomNumber}\n";
        }
        internal static string MenuShowAllFormatBooking(Booking booking)
        {
            return $"{booking.Id,10} |" +
                $" {booking.StartDate,10:yyyy-MM-dd} - {booking.EndDate,10:yyyy-MM-dd} |" +
                $"{booking.CustomerId,12} |" +
                $"{booking.Room.RoomNumber,8}";

        }
    }
}
