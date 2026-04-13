using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities.Printers
{
    internal static class CustomerPrinter
    {
        internal static string FormatCustomer(Customer customer)
        {
            return $"Customer ID: {customer.Id}" +
                $"Customer full name: {customer.FullName}" +
                $"Customer birth date: {customer.BirthDate:yyyy-MM-dd}" +
                $"Customer email: {customer.Email}" +
                $"Customer phone number: {customer.PhoneNumber}";
        }
        internal static void PrintCustomer(Customer customer)
        {
            Console.WriteLine(FormatCustomer(customer));
            Console.WriteLine();
        }
        internal static void PrintCustomers(List<Customer> customers)
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found");
            }
            else
            {
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(FormatCustomer(customer!));
                }
            }
        }
        internal static void PrintCustomerAndTheirBookings(Customer customer)
        {
            Console.WriteLine(FormatCustomer(customer!));

            if (customer.Bookings.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Bookings: ");

                foreach (Booking booking in customer.Bookings)
                {
                    Console.WriteLine(BookingPrinter.FormatBooking(booking!));
                }
            }
            Console.WriteLine();
        }
    }
}
