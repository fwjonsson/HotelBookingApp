using HotelBookingApp.Services.Customers;

namespace HotelBookingApp.Utilities.Printers.Customers
{
    internal static class CustomerValidationResultPrinter
    {
        internal static void PrintErrorsOrSuccess(CustomerValidationResult result, string messageOnSuccess)
        {
            if (result.Success)
            {
                Console.WriteLine(messageOnSuccess);
                return;
            }
            if (!result.CustomerFound)
            {
                Console.WriteLine("Customer not found.");
                return;
            }
            if (!result.IsAdult)
                Console.WriteLine("Customer must be at least 18 years old.");
            if (!result.EmailAvailable)
                Console.WriteLine("Email is already registered.");
            if (!result.PhoneNumberAvailable)
                Console.WriteLine("Phone number is already registered.");
            if (!result.CanBeDeleted)
                Console.WriteLine("Customers with current or future bookings cannot be deleted.");
        }
    }
}
