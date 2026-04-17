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
    internal class CreateBookingFlow
    {
        private readonly BookingService _bookingService;
        private readonly CreateBookingMenu _createBookingMenu;
        private readonly CustomerFlows _customerFlows;
        private readonly RoomFlows _roomFlows;

        internal CreateBookingFlow(BookingService bookingService, CustomerFlows customerFlows, RoomFlows roomFlows)
        {
            _bookingService = bookingService;
            _createBookingMenu = new CreateBookingMenu();
            _customerFlows = customerFlows;
            _roomFlows = roomFlows;
        }
        internal void Run()
        {
            Customer? customer = null;
            Room? room = null;
            DateOnly? startDate = null;
            DateOnly? endDate = null;

            while (true)
            {

                var choice = _createBookingMenu.Show(customer, room, startDate, endDate);
                switch (choice)
                {
                    case BookingEditorMenuOption.CustomerId:
                        var selectedCustomer = _customerFlows.RunAndGetCustomer();
                        if (selectedCustomer is not null)
                            customer = selectedCustomer;
                        break;

                    case BookingEditorMenuOption.StartDate:
                        DateOnly? startDateInput = BookingFlowHelper.TrySetBookingStartDate(endDate);
                        if (startDateInput is not DateOnly sDateInput)
                            break;

                        startDate = sDateInput;
                        room = null;
                        break;

                    case BookingEditorMenuOption.EndDate:
                        DateOnly? endDateInput = BookingFlowHelper.TrySetBookingEndDate(startDate);
                        if (endDateInput is not DateOnly eDateInput)
                            break;

                        endDate = eDateInput;
                        room = null;
                        break;

                    case BookingEditorMenuOption.Room:
                        if (startDate is not DateOnly sDate || endDate is not DateOnly eDate)
                        {
                            Console.WriteLine("Must select dates first.");
                            InputHelper.WaitForContinue();
                            break;
                        }
                        Room? selectedRoom = _roomFlows.RunAndGetRoom(sDate, eDate, room);
                        if (selectedRoom is not null)
                            room = selectedRoom;
                        break;

                    case BookingEditorMenuOption.Save:
                        if (TrySave(customer?.Id, room?.Id, startDate, endDate))
                        {
                            _createBookingMenu.ResetMenu();
                            return;
                        }
                        break;

                    case BookingEditorMenuOption.Cancel:
                        _createBookingMenu.ResetMenu();
                        return;
                }
            }
        }
        private bool TrySave(int? customerId, int? roomId, DateOnly? startDate, DateOnly? endDate)
        {
            if (customerId is not int cId ||
                roomId is not int rId ||
                startDate is not DateOnly sDate ||
                endDate is not DateOnly eDate)
            {
                Console.WriteLine("You must fill in all values before creating.");
                InputHelper.WaitForContinue();
                return false;
            }
            BookingValidationResult result = _bookingService.CreateBooking(cId, rId, sDate, eDate);
            BookingValidationResultPrinter.PrintErrorsOrSuccess(result, "Booking created.");
            InputHelper.WaitForContinue();

            return result.Success;
        }
    }
}