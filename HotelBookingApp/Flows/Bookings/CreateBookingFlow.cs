using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Flows.Rooms;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Services.Customers;
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
                        startDate = InputHelper.ReadDateOnly("Start date (yyyy-MM-dd): ");
                        if (startDate is null)
                            room = null;
                        break;

                    case BookingEditorMenuOption.EndDate:
                        endDate = InputHelper.ReadDateOnly("End date (yyyy-MM-dd): ");
                        if (endDate is null)
                            room = null;
                        break;

                    case BookingEditorMenuOption.RoomId:
                        if (startDate is not DateOnly sDate || endDate is not DateOnly eDate)
                        {
                            Console.WriteLine("Must choose dates first.");
                            InputHelper.WaitForContinue();
                            break;
                        }
                        var selectedRoom = _roomFlows.RunAndGetRoom(sDate, eDate);
                        if (selectedRoom is not null)
                            room = selectedRoom;
                        break;

                    case BookingEditorMenuOption.ExtraBeds:
                        int? ExtraBedsInput = RoomFlowHelpers.ValidateExtraBeds(room?.ExtraBeds, room?.RoomType);
                        if (ExtraBedsInput is int newExtraBeds)
                            room?.ExtraBeds = newExtraBeds;
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
            if (result.Success)
                return true;
            return false;
        }
    }
}