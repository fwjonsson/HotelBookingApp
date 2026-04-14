using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Bookings;

namespace HotelBookingApp.Flows.Bookings
{
    internal class UpdateBookingFlow
    {
        private readonly BookingService _bookingService;
        private readonly UpdateBookingMenu _updateBookingMenu;

        internal UpdateBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
            _updateBookingMenu = new UpdateBookingMenu();
        }
        internal void Run(int bookingId)
        {
            Booking? booking = _bookingService.GetById(bookingId);
            if (!FlowHelper.CheckItemExists(booking, "Booking not found."))
                return;
            DateOnly newStartDate = booking!.StartDate;
            DateOnly newEndDate = booking.EndDate;
            int newCustomerId = booking.CustomerId;
            int newRoomId = booking.RoomId;


            while (true)
            {
                BookingEditorMenuOption choice = _updateBookingMenu.Show(newStartDate, newEndDate, newRoomId, newCustomerId);

                switch (choice)
                {
                    case BookingEditorMenuOption.StartDate:
                        newStartDate = InputHelper.ReadDateOnly("Start date (yyyy-MM-dd): ");
                        continue;
                    case BookingEditorMenuOption.EndDate:
                        newEndDate = InputHelper.ReadDateOnly("End date (yyyy-MM-dd): ");
                        continue;
                    case BookingEditorMenuOption.RoomId:
                        newRoomId = InputHelper.ReadInt("Room (room ID): ");
                        continue;
                    case BookingEditorMenuOption.CustomerId:
                        newCustomerId = InputHelper.ReadInt("Customer (customer ID): ");
                        continue;
                    case BookingEditorMenuOption.Save:
                        bool saved = TrySave(bookingId, newStartDate, newEndDate, newCustomerId, newRoomId);
                        InputHelper.WaitForContinue();
                        if (saved)
                        {
                            _updateBookingMenu.ResetIndex();
                            return;
                        }
                        break;
                    case BookingEditorMenuOption.Cancel:
                        _updateBookingMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForContinue();
            }
        }

        private bool TrySave(int bookingId, DateOnly newStartDate, DateOnly newEndDate, int newCustomerId, int newRoomId)
        {
            BookingValidationResult result = _bookingService.UpdateBooking(bookingId, newStartDate, newEndDate, newRoomId, newCustomerId);
            BookingValidationResultPrinter.PrintErrorsOrSuccess(result, "Changes saved");
            InputHelper.WaitForContinue();
            if (result.Success)
                return true;
            return false;
        }
    }
}