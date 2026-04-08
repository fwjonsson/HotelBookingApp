using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services;
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
                    case CreateBookingMenuOption.CustomerId:
                        customerId = InputHelper.ReadInt("Enter customer ID: ");
                        continue;
                    case CreateBookingMenuOption.RoomId:
                        roomId = InputHelper.ReadInt("Enter room ID: ");
                        continue;
                    case CreateBookingMenuOption.StartDate:
                        startDate = InputHelper.ReadDateOnly("Enter start date (yyyy-MM-dd): ");
                        continue;
                    case CreateBookingMenuOption.EndDate:
                        endDate = InputHelper.ReadDateOnly("Enter end date (yyyy-MM-dd): ");
                        continue;
                    case CreateBookingMenuOption.Create:
                        BookingResult result = _bookingService.CreateBooking(customerId, roomId, startDate, endDate);
                        _createBookingMenu.ResetIndex();
                        break;
                    case CreateBookingMenuOption.Cancel:
                        _createBookingMenu.ResetIndex();
                        return;
                }
                
                InputHelper.WaitForBack();
            }
        }
    }
}