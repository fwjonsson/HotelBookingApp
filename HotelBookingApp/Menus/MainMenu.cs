using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Utilities;

namespace HotelBookingApp.Menus
{
    internal class MainMenu
    {
        private readonly ConsoleMenu<MainMenuOption> _menu;

        internal MainMenu()
        {
            _menu = new ConsoleMenu<MainMenuOption>("Main Menu", new List<MenuChoice<MainMenuOption>>
            {
                new(MainMenuOption.Bookings, "Bookings"),
                new(MainMenuOption.Customers, "Customers"),
                new(MainMenuOption.Rooms, "Rooms"),
                new(MainMenuOption.Exit, "Exit")
            });
        }
        internal MainMenuOption Show()
        {
            return _menu.Show();
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
