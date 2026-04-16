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
            Customer newCustomer = booking.Customer;
            Room newRoom = booking.Room;


            while (true)
            {
                BookingEditorMenuOption choice = _updateBookingMenu.Show(newStartDate, newEndDate, newRoom.Id, newCustomer.Id);

                switch (choice)
                {
                    case BookingEditorMenuOption.StartDate:
                        var newStartDateInput = BookingFlowHelper.TrySetBookingStartDate(newEndDate);

                        if (newStartDateInput is not DateOnly nStartDateInput)
                            break;

                        newStartDate = nStartDateInput;
                        break;

                    case BookingEditorMenuOption.EndDate:
                        var newEndDateInput = BookingFlowHelper.TrySetBookingEndDate(newStartDate);

                        if (newEndDateInput is not DateOnly nEndDateInput)
                            break;

                        newEndDate = nEndDateInput;
                        break;


                    case BookingEditorMenuOption.CustomerId:
                        Customer? selectedCustomer = _customerFlows.RunAndGetCustomer();
                        if (selectedCustomer is not null)
                            newCustomer = selectedCustomer;
                        break;

                    case BookingEditorMenuOption.RoomId:
                        Room? selectedRoom = _roomFlows.RunAndGetRoom(newStartDate, newEndDate, newRoom);
                        if (selectedRoom is not null)
                            newRoom = selectedRoom;
                        break;

                    case BookingEditorMenuOption.Save:
                        if (TrySave(bookingId, newCustomer.Id, newRoom.Id, newStartDate, newEndDate))
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