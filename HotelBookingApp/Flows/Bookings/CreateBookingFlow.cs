using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;

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
                        customerId = InputHelper.ReadInt("Enter customer ID: ");
                        continue;
                    case BookingEditorMenuOption.RoomId:
                        roomId = InputHelper.ReadInt("Enter room ID: ");
                        continue;
                    case BookingEditorMenuOption.StartDate:
                        startDate = InputHelper.ReadDateOnly("Enter start date (yyyy-MM-dd): ");
                        continue;
                    case BookingEditorMenuOption.EndDate:
                        endDate = InputHelper.ReadDateOnly("Enter end date (yyyy-MM-dd): ");
                        continue;
                    case BookingEditorMenuOption.Save:
                        if (customerId is not int cId ||
                            roomId is not int rId ||
                            startDate is not DateOnly sDate ||
                            endDate is not DateOnly eDate)
                        {
                            Console.WriteLine("You must fill in all values before creating.");
                            InputHelper.WaitForContinue();
                            continue;
                        }
                        BookingValidationResult result = _bookingService.CreateBooking(cId, rId, sDate, eDate);
                        if (!result.CustomerFound)
                        {
                            Console.WriteLine("Customer not found.");
                        }
                        if (!result.RoomFound)
                        {
                            Console.WriteLine("Room not found.");
                        }
                        if (!result.ValidDateRange)
                        {
                            Console.WriteLine("Start date cannot be earlier than today and end date must be after start date");
                        }
                        if (!result.RoomAvailable)
                        {
                            Console.WriteLine("Room unavailable for those dates.");
                        }
                        if (result.Success)
                        {
                            Console.WriteLine("Booking created successfully.");
                            _createBookingMenu.ResetIndex();
                            InputHelper.WaitForContinue();
                            return;
                        }
                        break;
                    case BookingEditorMenuOption.Cancel:
                        _createBookingMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForContinue();
            }
        }
    }
}