using HotelBookingApp.Enums.Menus.Bookings;
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

        internal ReadBookingFlow(BookingService bookingService)
        {
            _readBookingMenu = new ReadBookingMenu();

            _bookingService = bookingService;
        }
        internal void Run()
        {
            while (true)
            {
                ReadBookingMenuOption choice = _readBookingMenu.Show();

                switch (choice)
                {
                    case ReadBookingMenuOption.ReadAll:
                        ReadAll();
                        break;
                    case ReadBookingMenuOption.ReadByDate:
                        ReadByDate();
                        break;
                    case ReadBookingMenuOption.ReadById:
                        ReadById();
                        break;
                    case ReadBookingMenuOption.ReadByRoomId:
                        ReadByRoomId();
                        break;
                    case ReadBookingMenuOption.ReadByCustomerId:
                        ReadByCustomerId();
                        break;
                    case ReadBookingMenuOption.Back:
                        _readBookingMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForContinue();
            }
        }
        private void ReadAll()
        {
            List<Booking> allBookings = _bookingService.ListAll();
            BookingPrinter.PrintBookings(allBookings);
        }

        private void ReadByDate()
        {
            var date = InputHelper.ReadDateOnly("Booking date (yyyy-MM-dd):");
            List<Booking> bookingsOnDate = _bookingService.GetByDate(date);

            BookingPrinter.PrintBookings(bookingsOnDate);
        }

        private void ReadById()
        {
            int bookingId = InputHelper.ReadInt("Booking ID:");
            Booking? booking = _bookingService.GetById(bookingId);
            if (booking == null)
            {
                Console.WriteLine("No booking with that ID found.");
                return;
            }
            BookingPrinter.PrintBooking(booking);
        }

        private void ReadByRoomId()
        {
            int id = InputHelper.ReadInt("Room ID:");
            List<Booking> bookingsOnRoom = _bookingService.GetByRoomId(id);
            BookingPrinter.PrintBookings(bookingsOnRoom);
        }

        private void ReadByCustomerId()
        {
            int customerId = InputHelper.ReadInt("Customer ID:");
            List<Booking> bookingsByCustomer = _bookingService.GetByCustomerId(customerId);
            BookingPrinter.PrintBookings(bookingsByCustomer);
        }
    }
}