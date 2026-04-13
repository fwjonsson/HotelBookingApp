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
            return $"Booking ID: {booking.Id}" +
                $"Start Date: {booking.StartDate}" +
                $"End Date: {booking.EndDate}" +
                $"Customer ID: {booking.CustomerId}" +
                $"Room ID: {booking.RoomId}";

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
                    PrintBooking(booking);
                }
            }
        }
        internal static string MenuFormatBooking(Booking booking)
        {
            return $"{booking.Id} - {booking.StartDate} - {booking.EndDate} - {booking.CustomerId} - {booking.RoomId}";

        }
    }
}
