using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Flows.Rooms;
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
        private readonly CustomerFlows _customerFlows;
        private readonly RoomFlows _roomFlows;

        internal UpdateBookingFlow(BookingService bookingService, CustomerFlows customerFlows, RoomFlows roomFlows)
        {
            _bookingService = bookingService;
            _updateBookingMenu = new UpdateBookingMenu();
            _customerFlows = customerFlows;
            _roomFlows = roomFlows;
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
                        var newStartDateInput = BookingFlowHelper.TryUpdateBookingStartDate(newEndDate);

                        if (newStartDateInput is not DateOnly nStartDateInput)
                            break;

                        newStartDate = nStartDateInput;
                        break;

                    case BookingEditorMenuOption.EndDate:
                        var newEndDateInput = BookingFlowHelper.TryUpdateBookingEndDate(newStartDate);

                        if(newEndDateInput is not DateOnly nEndDateInput)
                        break;

                        newEndDate = nEndDateInput;
                        break;

                    case BookingEditorMenuOption.RoomId:
                        Room? room = _roomFlows.RunAndGetRoom(newStartDate, newEndDate, booking.Room);
                        if (room is Room)
                            newRoomId = room.Id;
                        break;

                    case BookingEditorMenuOption.CustomerId:
                        Customer? customer = _customerFlows.RunAndGetCustomer();
                        if (customer is Customer)
                            newCustomerId = customer.Id;
                        break;

                    case BookingEditorMenuOption.Save:
                        if (TrySave(bookingId, newCustomerId, newRoomId, newStartDate, newEndDate))
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

        private bool TrySave(int bookingId, int newCustomerId, int newRoomId, DateOnly newStartDate, DateOnly newEndDate)
        {
            BookingValidationResult result = _bookingService.UpdateBooking(bookingId, newRoomId, newCustomerId, newStartDate, newEndDate);
            BookingValidationResultPrinter.PrintErrorsOrSuccess(result, "Changes saved.");
            InputHelper.WaitForContinue();

            return result.Success;
        }
    }
}