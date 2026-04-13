using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers;

namespace HotelBookingApp.Flows.Bookings
{
    internal class DeleteBookingFlow
    {
        private readonly BookingService _bookingService;

        internal DeleteBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
        }
        internal void Run(int bookingId)
        {
            Booking? booking = _bookingService.GetById(bookingId);
            
            if (!FlowHelper.CheckItemExistsAndWait(booking, "Booking not found."))
                return;

            Console.WriteLine(BookingPrinter.FormatBooking(booking!));
            if (InputHelper.WaitForYesOrNo("Are you sure you want to delete? Y/N"))
            {
                BookingValidationResult result = _bookingService.DeleteBooking(booking!.Id);

                if (!result.BookingFound)
                {
                    Console.WriteLine("No booking with that ID found.");
                }
                if (result.Success)
                {
                    Console.WriteLine("Booking deleted.");
                }
            }
            InputHelper.WaitForContinue();
        }
    }
}
