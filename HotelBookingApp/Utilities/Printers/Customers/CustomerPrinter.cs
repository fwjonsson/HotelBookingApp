using HotelBookingApp.Models;
using HotelBookingApp.Utilities.Printers.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities.Printers.Customers
{
    internal static class CustomerPrinter
    {
        internal static string FormatCustomer(Customer customer)
        {
            return $"Customer ID: {customer.Id}\n" +
                $"Full name: {customer.FullName}\n" +
                $"Birth date: {customer.BirthDate:yyyy-MM-dd}\n" +
                $"Email: {customer.Email}\n" +
                $"Phone number: {customer.PhoneNumber}\n";
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
