using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
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
            }
        }
        private void ReadAll()
        {
            List<Booking> allBookings = _bookingService.ListAll();

            if (allBookings.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                InputHelper.WaitForBack();
                return;
            }
            BookingPrinter.PrintBookings(allBookings);
            InputHelper.WaitForBack();
        }

        private void ReadByDate()
        {
            var date = InputHelper.ReadDateOnly("Enter booking date (yyyy-MM-dd):");
            List<Booking> bookingsOnDate = _bookingService.GetByDate(date);
            if (bookingsOnDate.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                InputHelper.WaitForBack();
                return;
            }

            BookingPrinter.PrintBookings(bookingsOnDate);
            InputHelper.WaitForBack();
        }

        private void ReadById()
        {
            int id = InputHelper.ReadInt("Enter booking ID:");
            Booking? booking = _bookingService.GetById(id);
            if (booking == null)
            {
                Console.WriteLine("No booking with that ID");
                InputHelper.WaitForBack();
                return;
            }
            BookingPrinter.PrintBooking(booking);
            InputHelper.WaitForBack();
        }

        private void ReadByRoomId()
        {
            int id = InputHelper.ReadInt("Enter room ID:");
            List<Booking> bookingsOnRoom = _bookingService.GetByRoomId(id);
            if (bookingsOnRoom.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                InputHelper.WaitForBack();
                return;
            }
            BookingPrinter.PrintBookings(bookingsOnRoom);
            InputHelper.WaitForBack();
        }

        private void ReadByCustomerId()
        {
            int id = InputHelper.ReadInt("Enter customer ID:");
            List<Booking> bookingsByCustomer = _bookingService.GetByCustomerId(id);
            if (bookingsByCustomer.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                InputHelper.WaitForBack();
                return;
            }
            BookingPrinter.PrintBookings(bookingsByCustomer);
            InputHelper.WaitForBack();
        }
    }
}