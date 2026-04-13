using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services.Bookings;

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
                    case BookingMenuOption.ShowAll:
                        _read.ReadAll();
                        break;
                    case BookingMenuOption.FindById:
                        _read.ReadById();
                        break;
                    case BookingMenuOption.FindByDate:
                        _read.ReadByDate();
                        break;
                    case BookingMenuOption.FindByRoomId:
                        _read.ReadByRoomId();
                        break;
                    case BookingMenuOption.FindByCustomerId:
                        _read.ReadByCustomerId();
                        break;
                    case BookingMenuOption.Back:
                        _bookingMenu.ResetIndex();
                        return;
                }
            }
        }
    }
}
