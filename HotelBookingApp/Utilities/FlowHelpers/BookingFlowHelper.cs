using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities.Printers.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities.FlowHelpers
{
    internal static class BookingFlowHelper
    {
        internal static BookingValidationResult ValidateBookingDateInput(DateOnly? startDate, DateOnly? endDate)
        {
            var result = new BookingValidationResult();
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            if (startDate is not null && startDate < today ||
                startDate is null && endDate is not null && endDate <= today)
                result.ValidDateRange = false;

            if (startDate is not null && endDate is not null && endDate <= startDate)
                result.EndDateIsAfterStartDate = false;
            return result;
        }
        internal static DateOnly? TrySetBookingStartDate(DateOnly? newEndDate)
        {
            DateOnly? newStartDateInput = InputHelper.ReadDateOnly("Start date: ");
            if (newStartDateInput is not DateOnly nStartDateInput)
                return null;
            var result = ValidateBookingDateInput(nStartDateInput, newEndDate);
            BookingValidationResultPrinter.PrintErrorsOrSuccess(result, "");
            if (result.Success)
                return nStartDateInput;
            else
                InputHelper.WaitForContinue();
            return null;
        }
        internal static DateOnly? TrySetBookingEndDate(DateOnly? newStartDate)
        {
            DateOnly? newEndDateInput = InputHelper.ReadDateOnly("End date: ");
            if (newEndDateInput is not DateOnly nEndDateInput)
                return null;
            var result = ValidateBookingDateInput(newStartDate, nEndDateInput);
            BookingValidationResultPrinter.PrintErrorsOrSuccess(result, "");
            if (result.Success)
                return nEndDateInput;
            else
                InputHelper.WaitForContinue();
            return null;
        }
    }
}
