using System;
using System.Collections.Generic;
using System.Text;
using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services;

namespace HotelBookingApp.Flows.Bookings
{
    internal class BookingFlows
    {
        private readonly BookingMenu _bookingMenu;

        private readonly CreateBookingFlow _create;
        private readonly ReadBookingFlow _read;
        private readonly UpdateBookingFlow _update;
        private readonly DeleteBookingFlow _delete;

        internal BookingFlows(BookingService bookingService)
        {
            _bookingMenu = new BookingMenu();

            _create = new CreateBookingFlow(bookingService);
            _read = new ReadBookingFlow(bookingService);
            _update = new UpdateBookingFlow(bookingService);
            _delete = new DeleteBookingFlow(bookingService);
        }
        internal void Run()
        {
            while (true)
            {
                BookingMenuOption choice = _bookingMenu.Show();

                switch (choice)
                {
                    case BookingMenuOption.Create:
                        _create.Run();
                        break;
                    case BookingMenuOption.Read:
                        _read.Run();
                        break;
                    case BookingMenuOption.Update:
                        _update.Run();
                        break;
                    case BookingMenuOption.Delete:
                        _delete.Run();
                        break;
                    case BookingMenuOption.Back:
                        _bookingMenu.ResetIndex();
                        return;
                }
            }
        }
    }
}
