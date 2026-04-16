using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities.FlowHelpers
{
    internal static class BookingFlowHelper
    {
        internal static bool ValidateBookingDateInput(DateOnly? startDate, DateOnly? endDate)
        {
            bool success = true;
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            if (startDate is not null && startDate < today)
            {
                Console.WriteLine("Start date cannot be earlier than today.");
                success = false;
            }
            if (startDate is null && endDate is not null && endDate <= today)
            {
                Console.WriteLine("End date cannot be earlier than tomorrow.");
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
