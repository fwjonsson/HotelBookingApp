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
    }
}
