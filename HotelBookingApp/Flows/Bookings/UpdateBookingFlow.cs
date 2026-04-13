using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;

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
        internal void Run(int bookingId)
        {
            Booking? booking = _bookingService.GetById(bookingId);
            if (!FlowHelper.CheckItemExistsAndWait(booking, "Booking not found."))
                return;
            DateOnly newStartDate = booking!.StartDate;
            DateOnly newEndDate = booking.EndDate;
            int newCustomerId = booking.CustomerId;
            int newRoomId = booking.RoomId;


            while (true)
            {
                BookingEditorMenuOption choice = _updateBookingMenu.Show(newStartDate, newEndDate, newRoomId, newCustomerId);

                switch (choice)
                {
                    case BookingEditorMenuOption.StartDate:
                        newStartDate = InputHelper.ReadDateOnly("New start date (yyyy-MM-dd): ");
                        continue;
                    case BookingEditorMenuOption.EndDate:
                        newEndDate = InputHelper.ReadDateOnly("New end date (yyyy-MM-dd): ");
                        continue;
                    case BookingEditorMenuOption.RoomId:
                        newRoomId = InputHelper.ReadInt("New room ID: ");
                        continue;
                    case BookingEditorMenuOption.CustomerId:
                        newCustomerId = InputHelper.ReadInt("New customer ID: ");
                        continue;
                    case BookingEditorMenuOption.Save:
                        bool saved = TrySave(bookingId, newStartDate, newEndDate, newCustomerId, newRoomId);
                        InputHelper.WaitForContinue();
                        if (saved)
                        {
                            _updateBookingMenu.ResetIndex();
                            return;
                        }
                        break;
                    case BookingEditorMenuOption.Cancel:
                        _updateBookingMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForContinue();
            }
        }

        private bool TrySave(int bookingId, DateOnly newStartDate, DateOnly newEndDate, int newCustomerId, int newRoomId)
        {
            BookingValidationResult result = _bookingService.UpdateBooking(bookingId, newStartDate, newEndDate, newRoomId, newCustomerId);
            if (!result.BookingFound)
                Console.WriteLine("Booking not found.");
            if (!result.ValidDateRange)
                Console.WriteLine("Start date cannot be earlier than today and end date must be after start date");
            if (!result.CustomerFound)
                Console.WriteLine("Customer not found.");
            if (!result.RoomFound)
                Console.WriteLine("Room not found.");
            if (!result.RoomAvailable)
                Console.WriteLine("Room unavailable for those dates.");
            if (result.Success)
            {
                Console.WriteLine("Changes saved.");
                return true; ;
            }
            return false;
        }
    }
}