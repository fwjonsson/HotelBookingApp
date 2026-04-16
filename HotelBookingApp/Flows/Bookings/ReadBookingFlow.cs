using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Bookings;

namespace HotelBookingApp.Flows.Bookings
{
    internal class ReadBookingFlow
    {
        private readonly BookingService _bookingService;
        private readonly SelectObjectMenu<Booking> _selectBookingMenu;
        private readonly ActionMenu<Booking> _bookingActionMenu;
        private readonly UpdateBookingFlow _updateBookingFlow;
        private readonly DeleteBookingFlow _deleteBookingFlow;

        internal ReadBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
            _selectBookingMenu = new SelectObjectMenu<Booking>();
            _bookingActionMenu = new ActionMenu<Booking>("--Booking--", BookingPrinter.FormatBooking);
            _updateBookingFlow = new UpdateBookingFlow(bookingService);
            _deleteBookingFlow = new DeleteBookingFlow(bookingService);

        }
        private void SelectBookingAndChooseAction(Func<List<Booking>> getBookings, Func<Booking, string> formatter, string title)
        {
            while (true)
            {
                List<Booking> bookings = getBookings();

                if (bookings.Count == 0)
                {
                    Console.WriteLine("No bookings found.");
                    InputHelper.WaitForContinue();
                    return;
                }

                Booking? booking = _selectBookingMenu.Show(bookings, formatter, title);
                if (booking == null)
                {
                    _selectBookingMenu.ResetMenu();
                    return;
                }

                ChooseAction(booking.Id);
            }
        }
        private void ChooseAction(int bookingId)
        {
            while (true)
            {
                Booking? booking = _bookingService.GetById(bookingId);

                if (!FlowHelper.CheckItemExists(booking, "Booking not found"))
                    return;

                ActionMenuOption choice = _bookingActionMenu.Show(booking!);

                switch (choice)
                {
                    case ActionMenuOption.Edit:
                        _updateBookingFlow.Run(bookingId);
                        break;

                    case ActionMenuOption.Delete:
                        if (_deleteBookingFlow.TryDelete(bookingId))
                        {
                            _bookingActionMenu.ResetMenu();
                            return;
                        }
                        break;

                    case ActionMenuOption.Back:
                        _bookingActionMenu.ResetMenu();
                        return;
                }
            }
        }
        internal void ReadAll()
        {
            SelectBookingAndChooseAction(_bookingService.ListAll, BookingPrinter.MenuShowAllFormatBooking,
                "--All Bookings--\n" +
                "     ID |      Booked Dates      | Customer ID | Room ID\n" + new string('-', 60));
        }

        internal void ReadByDate()
        {
            DateOnly? dateInput = InputHelper.ReadDateOnly("Booking Date: ");
            if (dateInput is not DateOnly date)
                return;
            SelectBookingAndChooseAction(() => _bookingService.GetByDate(date), BookingPrinter.MenuShowAllFormatBooking,
                $"--Bookings On Date: {date:yyyy-MM-dd}--\n" +
                "     ID |      Booked Dates      | Customer ID | Room ID\n" + new string('-', 60));
        }

        internal void ReadById()
        {
            int? bookingIdInput = InputHelper.ReadInt("Booking ID: ");
            if (bookingIdInput is not int bookingId)
                return;
            ChooseAction(bookingId);
        }

        internal void ReadByRoomId()
        {
            int? roomIdInput = InputHelper.ReadInt("Room ID: ");
            if (roomIdInput is not int roomId)
                return;
            SelectBookingAndChooseAction(() => _bookingService.GetByRoomId(roomId), BookingPrinter.MenuShowAllFormatBooking,
                $"--Bookings On Room ID: {roomId}--\n" +
                 "     ID |      Booked Dates      | Customer ID | Room ID\n" + new string('-', 60));
        }

        internal void ReadByCustomerId()
        {
            int? customerIdInput = InputHelper.ReadInt("Customer ID: ");
            if (customerIdInput is not int customerId)
                return;
            SelectBookingAndChooseAction(() => _bookingService.GetByCustomerId(customerId), BookingPrinter.MenuShowAllFormatBooking,
                $"--Bookings By Customer ID: {customerId}--\n" +
                "     ID |      Booked Dates      | Customer ID | Room ID\n" + new string('-', 60));

        }
    }
}