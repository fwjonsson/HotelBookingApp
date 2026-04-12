using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Utilities;

namespace HotelBookingApp.Flows.Bookings
{
    internal class ReadBookingFlow
    {
        private readonly BookingService _bookingService;
        private readonly ReadBookingMenu _readBookingMenu;
        private readonly SelectObjectMenu<Booking> _selectBookingMenu;
        private readonly ActionMenu<Booking> _bookingActionMenu;
        private readonly UpdateBookingFlow _updateBookingFlow;
        private readonly DeleteBookingFlow _deleteBookingFlow;

        internal ReadBookingFlow(BookingService bookingService)
        {
            _bookingService = bookingService;
            _readBookingMenu = new ReadBookingMenu();
            _selectBookingMenu = new SelectObjectMenu<Booking>("Booking", BookingPrinter.FormatBooking);
            _bookingActionMenu = new ActionMenu<Booking>("Booking", BookingPrinter.FormatBooking);
            _updateBookingFlow = new UpdateBookingFlow(bookingService);
            _deleteBookingFlow = new DeleteBookingFlow(bookingService);

        }
        //internal void Run()
        //{
        //    while (true)
        //    {
        //        ReadBookingMenuOption choice = _readBookingMenu.Show();

        //        switch (choice)
        //        {
        //            case ReadBookingMenuOption.ReadAll:
        //                ReadAll();
        //                break;
        //            case ReadBookingMenuOption.ReadByDate:
        //                ReadByDate();
        //                break;
        //            case ReadBookingMenuOption.ReadById:
        //                ReadById();
        //                break;
        //            case ReadBookingMenuOption.ReadByRoomId:
        //                ReadByRoomId();
        //                break;
        //            case ReadBookingMenuOption.ReadByCustomerId:
        //                ReadByCustomerId();
        //                break;
        //            case ReadBookingMenuOption.Back:
        //                _readBookingMenu.ResetIndex();
        //                return;
        //        }
        //        InputHelper.WaitForContinue();
        //    }
        //}
        private bool SelectBookingAndChooseAction(List<Booking> bookings)
        {
                if (bookings.Count == 0)
                {
                    Console.WriteLine("No bookings found.");
                    return false;
                }

                Booking? booking = _selectBookingMenu.Show(bookings);
                if (booking == null)
                {
                    _selectBookingMenu.ResetIndex();
                    return false;
                }
                ChooseAction(booking);
            return true;
        }
        private void ChooseAction(Booking booking)
        {
                ActionMenuOption choice = _bookingActionMenu.Show(booking);
                switch (choice)
                {
                    case ActionMenuOption.Edit:
                        _updateBookingFlow.Run(booking);
                        break;
                    case ActionMenuOption.Delete:
                        _deleteBookingFlow.Run(booking);
                        break;
                    case ActionMenuOption.Back:
                        _bookingActionMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForContinue();
        }
        internal void ReadAll() //TODO CONTINUE FINISHING THE READ METHODS
        {
            var inMenu = true;
            while (inMenu)
            {
                List<Booking> allBookings = _bookingService.ListAll();
                inMenu = SelectBookingAndChooseAction(allBookings);
            }
        }

        internal void ReadByDate()
        {
            var date = InputHelper.ReadDateOnly("Booking date (yyyy-MM-dd):");
            var inMenu = true;
            while (inMenu)
            {
            List<Booking> bookingsOnDate = _bookingService.GetByDate(date);
                inMenu = SelectBookingAndChooseAction(bookingsOnDate);
            }
        }

        internal void ReadById()
        {
            int bookingId = InputHelper.ReadInt("Booking ID:");
            Booking? booking = _bookingService.GetById(bookingId);
            if (booking == null)
            {
                Console.WriteLine("No booking with that ID found.");
                return;
            }
            ChooseAction(booking);
        }

        internal void ReadByRoomId()
        {
            int id = InputHelper.ReadInt("Room ID:");
            
            List<Booking> bookingsOnRoom = _bookingService.GetByRoomId(id);
            SelectBookingAndChooseAction(bookingsOnRoom);
        }

        internal void ReadByCustomerId()
        {
            int customerId = InputHelper.ReadInt("Customer ID:");
            List<Booking> bookingsByCustomer = _bookingService.GetByCustomerId(customerId);
            SelectBookingAndChooseAction(bookingsByCustomer);
        }
    }
}