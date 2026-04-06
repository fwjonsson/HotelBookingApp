using HotelBookingApp.Data;
using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Flows.Bookings;
using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Flows.Rooms;
using HotelBookingApp.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp
{
    internal class App
    {
        private readonly BookingFlows _bookingFlows;
        private readonly CustomerFlows _customerFlows;
        private readonly RoomFlows _roomFlows;
        private readonly MainMenu _mainMenu;


        internal App(
            BookingFlows bookingFlows,
            CustomerFlows customerFlows,
            RoomFlows roomFlows
            )
        {
            _mainMenu = new MainMenu();

            _bookingFlows = bookingFlows;
            _customerFlows = customerFlows;
            _roomFlows = roomFlows;
        }
        internal void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                MainMenuOption choice = _mainMenu.Show();

                switch (choice)
                {
                    case MainMenuOption.Bookings:
                        _bookingFlows.Run();
                        break;
                    case MainMenuOption.Customers:
                        _customerFlows.Run();
                        break;
                    case MainMenuOption.Rooms:
                        _roomFlows.Run();
                        break;
                    case MainMenuOption.Exit:
                        isRunning = false;
                        break;
                }
            }
        }
    }
}
