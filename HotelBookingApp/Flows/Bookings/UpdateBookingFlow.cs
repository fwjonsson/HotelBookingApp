using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services;
using HotelBookingApp.Utilities;
using HotelBookingApp.Models;

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
            var id = InputHelper.ReadInt("Enter booking ID: ");
            Booking? booking = _bookingService.GetById(id);
            if (booking == null)
            {
                Console.WriteLine("No booking with that ID found.");
                BookingPrinter.WaitForBack();
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
                        _bookingService.UpdateBooking(booking, newStartDate, newEndDate, newRoomId, newCustomerId);
                        _updateBookingMenu.ResetIndex();
                        return;
                    case UpdateBookingMenuOption.Back:
                        _updateBookingMenu.ResetIndex();
                        return;

                }
            }
        }
    }
}