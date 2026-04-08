using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services;
using HotelBookingApp.Utilities;
using HotelBookingApp.Models;
using HotelBookingApp.Enums;

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
                UpdateBookingMenuOption choice = _updateBookingMenu.Show(newStartDate, newEndDate, newRoomId, newCustomerId);

                switch (choice)
                {
                    case UpdateBookingMenuOption.StartDate:
                        newStartDate = InputHelper.ReadDateOnly("New start date (yyyy-MM-dd): ");
                        break;
                    case UpdateBookingMenuOption.EndDate:
                        newEndDate = InputHelper.ReadDateOnly("New end date (yyyy-MM-dd): ");
                        break;
                    case UpdateBookingMenuOption.RoomId:
                        newRoomId = InputHelper.ReadInt("New room ID: ");
                        break;
                    case UpdateBookingMenuOption.CustomerId:
                        newCustomerId = InputHelper.ReadInt("New customer ID: ");
                        break;
                    case UpdateBookingMenuOption.Save:
                        BookingResult result = SaveChanges(bookingId, newStartDate, newEndDate, newRoomId, newCustomerId);
                        if (result == BookingResult.Success)
                        {
                            _updateBookingMenu.ResetIndex();
                            return;
                        }
                        break;
                    case UpdateBookingMenuOption.Back:
                        _updateBookingMenu.ResetIndex();
                        return;

                }
            }
        }
        private BookingResult SaveChanges(int bookingId, DateOnly newStartDate, DateOnly newEndDate, int newRoomId, int newCustomerId)
        {
            BookingResult result = _bookingService.UpdateBooking(bookingId, newStartDate, newEndDate, newRoomId, newCustomerId);
            
            switch (result)
            {
                case BookingResult.Success:
                    Console.WriteLine("Changes saved.");
                    break;
                case BookingResult.BookingNotFound:
                    Console.WriteLine("Booking not found.");
                    break;
                case BookingResult.InvalidDateRange:
                    Console.WriteLine("End date must be after start date.");
                    break;
                case BookingResult.CustomerNotFound:
                    Console.WriteLine("Customer not found.");
                    break;
                case BookingResult.RoomNotFound:
                    Console.WriteLine("Room not found.");
                    break;
                case BookingResult.RoomUnavailable:
                    Console.WriteLine("Room unavailable these dates.");
                    break;
            }
            InputHelper.WaitForBack();
            return result;
        }
    }
}