using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers;
using HotelBookingApp.Utilities.Printers.Bookings;

namespace HotelBookingApp.Flows.Bookings
{
    internal class DeleteBookingFlow
    {
        private readonly BookingService _bookingService;

        internal DeleteBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        internal bool TryDelete(int bookingId)
        {
            if (InputHelper.WaitForYesOrNo("Are you sure you want to delete? Y/N"))
            {
                Booking? booking = _bookingService.GetById(bookingId);

                BookingValidationResult result = _bookingService.DeleteBooking(booking!.Id);
                BookingValidationResultPrinter.PrintErrorsOrSuccess(result, "Booking deleted");
                InputHelper.WaitForContinue();
                if (result.Success)
                    return true;
            }
            return false;
        }
    }
}

