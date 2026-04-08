using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services;
using HotelBookingApp.Utilities;
using HotelBookingApp.Models;
using HotelBookingApp.Enums;
using HotelBookingApp.Services.Bookings;

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
        internal void Run()
        {
            var bookingId = InputHelper.ReadInt("Enter booking ID: ");
            Booking? booking = _bookingService.GetById(bookingId);
            if (booking == null)
            {
                Console.WriteLine("No booking with that ID found.");
                InputHelper.WaitForBack();
                return;
            }

            DateOnly newStartDate = booking.StartDate;
            DateOnly newEndDate = booking.EndDate;
            int newCustomerId = booking.CustomerId;
            int newRoomId = booking.RoomId;


            while (true)
            {
                booking = _bookingService.GetById(bookingId);
                if (booking == null)
                {
                    Console.WriteLine("No booking with that ID found.");
                    InputHelper.WaitForBack();
                    return;
                }
                UpdateBookingMenuOption choice = _updateBookingMenu.Show(newStartDate, newEndDate, newRoomId, newCustomerId);
                
                switch (choice)
                {
                    case UpdateBookingMenuOption.StartDate:
                        newStartDate = InputHelper.ReadDateOnly("New start date (yyyy-MM-dd): ");
                        continue;
                    case UpdateBookingMenuOption.EndDate:
                        newEndDate = InputHelper.ReadDateOnly("New end date (yyyy-MM-dd): ");
                        continue;
                    case UpdateBookingMenuOption.RoomId:
                        newRoomId = InputHelper.ReadInt("New room ID: ");
                        continue;
                    case UpdateBookingMenuOption.CustomerId:
                        newCustomerId = InputHelper.ReadInt("New customer ID: ");
                        continue;
                    case UpdateBookingMenuOption.Save:
                        var result = _bookingService.UpdateBooking(bookingId, newStartDate, newEndDate, newRoomId, newCustomerId);
                        if (!result.BookingFound)
                        {
                            Console.WriteLine("Booking not found.");
                        }
                        if (!result.ValidDateRange)
                        {
                            Console.WriteLine("Invalid date range.");
                            newStartDate = booking.StartDate;
                            newEndDate = booking.EndDate;
                        }
                        if (!result.CustomerFound)
                        {
                            Console.WriteLine("Customer not found.");
                            newCustomerId = booking.CustomerId;
                        }
                        if (!result.RoomFound)
                        {
                            Console.WriteLine("Room not found.");
                            newRoomId = booking.RoomId;
                        }
                        if (!result.RoomAvailable)
                        {
                            Console.WriteLine("Room unavailable.");
                        }
                        if (result.Success)
                        {
                            Console.WriteLine("Changes saved.");
                            InputHelper.WaitForBack();
                            _updateBookingMenu.ResetIndex();
                            return;
                        }
                        break;
                    case UpdateBookingMenuOption.Back:
                        _updateBookingMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForBack();
            }
        }
    }
}