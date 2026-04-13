using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;
using Microsoft.EntityFrameworkCore;

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
                        break;
                    case BookingEditorMenuOption.RoomId:
                        roomId = InputHelper.ReadInt("Enter room ID: ");
                        break;
                    case BookingEditorMenuOption.StartDate:
                        startDate = InputHelper.ReadDateOnly("Enter start date (yyyy-MM-dd): ");
                        break;
                    case BookingEditorMenuOption.EndDate:
                        endDate = InputHelper.ReadDateOnly("Enter end date (yyyy-MM-dd): ");
                        break;
                    case BookingEditorMenuOption.Save:
                        bool saved = TrySave(customerId, roomId, startDate, endDate);
                        InputHelper.WaitForContinue();
                        if (saved)
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
                return false;
            }
            BookingValidationResult result = _bookingService.CreateBooking(cId, rId, sDate, eDate);
            if (!result.CustomerFound)
                Console.WriteLine("Customer not found.");
            if (!result.RoomFound)
                Console.WriteLine("Room not found.");
            if (!result.ValidDateRange)
                Console.WriteLine("Start date cannot be earlier than today and end date must be after start date");
            if (!result.RoomAvailable)
                Console.WriteLine("Room unavailable for those dates.");
            if (result.Success)
            {
                Console.WriteLine("Booking created successfully.");
                return true;
            }
            return false;
        }
    }
}