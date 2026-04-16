using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Flows.Rooms;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Services.Rooms;

namespace HotelBookingApp.Flows.Bookings
{
    internal class BookingFlows
    {
        private readonly BookingMenu _bookingMenu;
        private readonly CreateBookingFlow _create;
        private readonly ReadBookingFlow _read;

        internal BookingFlows(BookingService bookingService, CustomerFlows customerFlows, RoomFlows roomFlows)
        {
            _bookingMenu = new BookingMenu();
            _create = new CreateBookingFlow(bookingService, customerFlows, roomFlows);
            _read = new ReadBookingFlow(bookingService, customerFlows, roomFlows);
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

                    case BookingMenuOption.ShowAll:
                        _read.ReadAll();
                        break;

                    case BookingMenuOption.FindByDate:
                        _read.ReadByDate();
                        break;

                    case BookingMenuOption.FindById:
                        _read.ReadById();
                        break;

                    case BookingMenuOption.FindByRoomId:
                        _read.ReadByRoomId();
                        break;

                    case BookingMenuOption.FindByCustomerId:
                        _read.ReadByCustomerId();
                        break;

                    case BookingMenuOption.Back:
                        _bookingMenu.ResetMenu();
                        return;
                }
            }
        }
    }
}
