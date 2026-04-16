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
                        DateOnly? newStartDateInput = InputHelper.ReadDateOnly("Start date: ");
                        if (newStartDateInput is not DateOnly nStartDateInput)
                            break;
                        if (BookingFlowHelper.ValidateBookingDateInput(nStartDateInput, newEndDate))
                            newStartDate = nStartDateInput;
                        break;

                    case BookingEditorMenuOption.EndDate:
                        DateOnly? newEndDateInput = InputHelper.ReadDateOnly("End date: ");
                        if (newEndDateInput is not DateOnly nEndDateInput)
                            break;
                        if (BookingFlowHelper.ValidateBookingDateInput(newStartDate, nEndDateInput))
                            newEndDate = nEndDateInput;
                        break;

                    case BookingEditorMenuOption.RoomId:
                        int? newRoomIdInput = InputHelper.ReadInt("Room (room ID): ");
                        if (newRoomIdInput is int nRoomId)
                            newRoomId = nRoomId;
                        break;

                    case BookingEditorMenuOption.CustomerId:
                        int? newCustomerIdInpud = InputHelper.ReadInt("Customer (customer ID): ");
                        if (newCustomerIdInpud is int nCustomerId)
                            newCustomerId = nCustomerId;
                        break;

                    case BookingEditorMenuOption.Save:
                        if (TrySave(bookingId, newStartDate, newEndDate, newCustomerId, newRoomId))
                        {
                            _updateBookingMenu.ResetIndex();
                            return;
                        }
                        break;

                    case BookingEditorMenuOption.Cancel:
                        _updateBookingMenu.ResetIndex();
                        return;
                }
            }
        }

        private bool TrySave(int bookingId, DateOnly newStartDate, DateOnly newEndDate, int newCustomerId, int newRoomId)
        {
            BookingValidationResult result = _bookingService.UpdateBooking(bookingId, newStartDate, newEndDate, newRoomId, newCustomerId);
            BookingValidationResultPrinter.PrintErrorsOrSuccess(result, "Changes saved.");
            InputHelper.WaitForContinue();

            return result.Success;
        }
    }
}