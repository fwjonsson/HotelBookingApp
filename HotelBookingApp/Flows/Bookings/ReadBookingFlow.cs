using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.Printers;

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
            _selectBookingMenu = new SelectObjectMenu<Booking>("Booking", BookingPrinter.FormatBooking);
            _bookingActionMenu = new ActionMenu<Booking>("Booking", BookingPrinter.FormatBooking);
            _updateBookingFlow = new UpdateBookingFlow(bookingService);
            _deleteBookingFlow = new DeleteBookingFlow(bookingService);

        }
        private void SelectBookingAndChooseAction(Func<List<Booking>> getBookings)
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

                Booking? booking = _selectBookingMenu.Show(bookings);
                if (booking == null)
                {
                    _selectBookingMenu.ResetIndex();
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
                FlowHelper.EnsureFound(booking, "Booking not found");
                if (booking == null)
                    return;

                ActionMenuOption choice = _bookingActionMenu.Show(booking);
                switch (choice)
                {
                    case ActionMenuOption.Edit:
                        _updateBookingFlow.Run(bookingId);
                        break;
                    case ActionMenuOption.Delete:
                        _deleteBookingFlow.Run(bookingId);
                        _bookingActionMenu.ResetIndex();
                        return;
                    case ActionMenuOption.Back:
                        _bookingActionMenu.ResetIndex();
                        return;
                }
            }
        }
        internal void ReadAll()
        {
            SelectBookingAndChooseAction(_bookingService.ListAll);
        }

        internal void ReadByDate()
        {
            var date = InputHelper.ReadDateOnly("Booking date (yyyy-MM-dd):");
            SelectBookingAndChooseAction(() => _bookingService.GetByDate(date));
        }

        internal void ReadById()
        {
            int bookingId = InputHelper.ReadInt("Booking ID:");
            ChooseAction(bookingId);
        }

        internal void ReadByRoomId()
        {
            int id = InputHelper.ReadInt("Room ID:");
            SelectBookingAndChooseAction(() => _bookingService.GetByRoomId(id));
        }

        internal void ReadByCustomerId()
        {
            int customerId = InputHelper.ReadInt("Customer ID:");
            SelectBookingAndChooseAction(() => _bookingService.GetByCustomerId(customerId));

        }
    }
}