using HotelBookingApp.Enums.Menus.Bookings;
using HotelBookingApp.Menus.Bookings;
using HotelBookingApp.Services;
using HotelBookingApp.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                        _bookingService.GetByRoomId(int roomId);
                        break;
                    case ReadBookingMenuOption.ReadByCustomerId:
                        _bookingService.GetByCustomerId(int customerId);
                        break;
                    case ReadBookingMenuOption.Back:
                        _readBookingMenu.ResetIndex();
                        return;
                }
            }
        }
        private void ReadAll()
        {
            _bookingService.ListAll();
        }
        private void ReadByDate()
        {
            var date = InputHelper.ReadDateOnly("Enter booking date:");
            _bookingService.GetByDate(DateOnly date);
        }
        private void ReadById()
        {
            int id = InputHelper.ReadInt("Enter booking id:");
            _bookingService.GetById(id);
        }
        private void ReadByRoomId()
        {

        }
        private void ReadByCustomerId()
        {

        }

    }
}