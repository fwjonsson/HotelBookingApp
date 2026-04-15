using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Flows.Rooms;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
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
            int? customerId = null;
            int? roomId = null;
            DateOnly? startDate = null;
            DateOnly? endDate = null;

            while (true)
            {

                var choice = _createBookingMenu.Show(customerId, roomId, startDate, endDate);
                switch (choice)
                {
                    case BookingEditorMenuOption.CustomerId:
                        var customer = _customerFlows.RunAndGetCustomer();
                        if (customer is not null)
                            customerId = customer.Id;
                        break;

                    case BookingEditorMenuOption.RoomId:
                        _roomFlows.Run();
                        break;

                    case BookingEditorMenuOption.StartDate:
                        startDate = InputHelper.ReadDateOnly("Start date (yyyy-MM-dd): ");
                        break;

                    case BookingEditorMenuOption.EndDate:
                        endDate = InputHelper.ReadDateOnly("End date (yyyy-MM-dd): ");
                        break;

                    case BookingEditorMenuOption.Save:
                        if (TrySave(customerId, roomId, startDate, endDate))
                        {
                            _createBookingMenu.ResetIndex();
                            return;
                        }
                        break;

                    case BookingEditorMenuOption.Cancel:
                        _createBookingMenu.ResetIndex();
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