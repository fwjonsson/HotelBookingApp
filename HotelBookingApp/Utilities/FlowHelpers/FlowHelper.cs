namespace HotelBookingApp.Utilities.FlowHelpers
{
    internal static class FlowHelper
    {
        internal static bool CheckItemExists<T>(T? item, string notFoundMessage) where T : class
        {
            if (item == null)
            {
                Console.WriteLine(notFoundMessage);
                InputHelper.WaitForContinue();
                return false;
            }
            return true;
        }
        internal static bool CheckBookingStartDate(DateOnly startDate, DateOnly? endDate)
        {
            bool success = true;
            if (startDate < DateOnly.FromDateTime(DateTime.Today))
            {
                Console.WriteLine("Start date cannot be earlier than today.");
                success = false;
            }

            if (endDate is not null && endDate <= startDate)
            {
                Console.WriteLine("End date must be after start date.");
                success = false;
            }
            return success;
        }

        internal static bool ValidateBookingDateInput(DateOnly? startDate, DateOnly? endDate)
        {
            bool success = true;
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            if (startDate is not null && startDate < today)
            {
                Console.WriteLine("Start date cannot be earlier than today.");
                success = false;
            }
            if (endDate is not null && endDate <= today)
            {
                Console.WriteLine("End date cannot be earlier than tomorrow");
                success = false;
            }
            if (startDate is not null && endDate is not null && endDate <= startDate)
            {
                Console.WriteLine("End date must be after start date.");
                success = false;
            }
            return success;
        }
    }
}

