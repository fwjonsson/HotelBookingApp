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
                $"Room ID: {booking.RoomId}\n";

        }
        internal static string MenuFormatBooking(Booking booking)
        {
            return $"Booking ID: {booking.Id}\n" +
                $"  Start Date: {booking.StartDate}\n" +
                $"  End Date: {booking.EndDate}\n" +
                $"  Customer ID: {booking.CustomerId}\n" +
                $"  Room ID: {booking.RoomId}\n";
        }
        //internal static string MenuFormatBooking(Booking booking)
        //{
        //    return $"Booking ID: {booking.Id}" +
        //        $"  Dates: {booking.StartDate} - {booking.EndDate}" +
        //        $"  Customer ID: {booking.CustomerId}" +
        //        $"  Room ID: {booking.RoomId}";

        //}
    }
}
