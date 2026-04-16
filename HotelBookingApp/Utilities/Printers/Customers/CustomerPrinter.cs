using HotelBookingApp.Models;

namespace HotelBookingApp.Utilities.Printers.Customers
{
    internal static class CustomerPrinter
    {
        internal static string FormatCustomer(Customer customer)
        {
            return $"Customer ID: {customer.Id}\n" +
                $"Full Name: {customer.FullName}\n" +
                $"Birth Date: {customer.BirthDate:yyyy-MM-dd}\n" +
                $"Email: {customer.Email}\n" +
                $"Phone Number: {customer.PhoneNumber}\n";
        }
        internal static string MenuFormatCustomer(Customer customer)
        {
            return $"Customer ID: {customer.Id}\n" +
                $"  Full Name: {customer.FullName}\n" +
                $"  Birth Date: {customer.BirthDate:yyyy-MM-dd}\n" +
                $"  Email: {customer.Email}\n" +
                $"  Phone Number: {customer.PhoneNumber}\n";
        }
        internal static string MenuShowAllFormatCustomer(Customer customer)
        {
            return $"{customer.Id,5} |" +
                $"{customer.FullName,30} |" +
                $"{customer.BirthDate,11:yyyy-MM-dd} |" +
                $"{customer.Email,30} |" +
                $"{customer.PhoneNumber,15}";
        }
    }
}
