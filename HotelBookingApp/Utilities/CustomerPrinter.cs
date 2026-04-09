using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities
{
    internal static class CustomerPrinter
    {
        internal static void PrintCustomer(Customer customer)
        {
            Console.WriteLine($"Customer ID: {customer.Id}");
            Console.WriteLine($"Customer full name: {customer.FullName}");
            Console.WriteLine($"Customer birth date: {customer.BirthDate:yyyy-MM-dd}");
            Console.WriteLine($"Customer email: {customer.Email}");
            Console.WriteLine($"Customer phone number: {customer.PhoneNumber}");
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
                    PrintCustomer(customer);
                }
            }
        }
        internal static void PrintCustomerAndTheirBookings(Customer customer)
        {
            PrintCustomer(customer);

            if (customer.Bookings.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"{customer.FullName}'s bookings: ");

                foreach (Booking booking in customer.Bookings)
                {
                    BookingPrinter.PrintBooking(booking);
                }
            }
            Console.WriteLine();
        }
    }
}
