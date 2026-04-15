using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.Printers.Bookings;

namespace HotelBookingApp.Flows.Bookings
{
    internal class CreateBookingFlow
    {
        private readonly BookingService _bookingService;
        private readonly CreateBookingMenu _createBookingMenu;

        internal CreateBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
            _createBookingMenu = new CreateBookingMenu();
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
                        customerId = InputHelper.ReadInt("Customer ID: ");
                        break;

                    case BookingEditorMenuOption.RoomId:
                        roomId = InputHelper.ReadInt("Room ID: ");
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