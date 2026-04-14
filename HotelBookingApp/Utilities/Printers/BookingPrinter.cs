using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities.Printers
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
        internal static void PrintBooking(Booking booking)
        {
            Console.WriteLine(FormatBooking(booking));
            Console.WriteLine();
        }
        internal static void PrintBookings(List<Booking> bookings)
        {
            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings found.");
            }

            else
            {
                foreach (var booking in bookings)
                {
                    Console.WriteLine(FormatBooking(booking!));
                }
            }
        }
        internal static string MenuFormatBooking(Booking booking)
        {
            return $"{booking.Id} - {booking.StartDate} - {booking.EndDate} - {booking.CustomerId} - {booking.RoomId}";

        }
    }
}
