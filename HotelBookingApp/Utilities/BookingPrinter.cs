using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities
{
    internal static class BookingPrinter
    {
        internal static void PrintBooking(Booking booking)
        {
            Console.WriteLine($"Booking ID: {booking.Id}");
            Console.WriteLine($"Start Date: {booking.StartDate}");
            Console.WriteLine($"End Date: {booking.EndDate}");
            Console.WriteLine($"Customer ID: {booking.CustomerId}");
            Console.WriteLine($"Room ID: {booking.RoomId}");
            Console.WriteLine();
        }
        internal static void PrintBookings(List<Booking> bookings)
        {
            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings found");
            }

            else
            {
                foreach (var booking in bookings)
                {
                    PrintBooking(booking);
                }
            }
        }

    }
}
