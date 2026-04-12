using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{
    internal class ReadCustomerMenu
    {
        private readonly ConsoleMenu<ReadCustomerMenuOption> _menu;

        internal ReadCustomerMenu()
        {
            _menu = new ConsoleMenu<ReadCustomerMenuOption>("Read Customer Menu", new List<MenuChoice<ReadCustomerMenuOption>>
            {
                new(ReadCustomerMenuOption.ReadAll, "Show all customers"),
                new(ReadCustomerMenuOption.ReadById, "Find customer by ID"),
                new(ReadCustomerMenuOption.ReadByFullName, "Find customer by name"),
                new(ReadCustomerMenuOption.ReadByBirthDate, "Find customer by birthdate"),
                new(ReadCustomerMenuOption.ReadByEmail, "Find customer by email"),
                new(ReadCustomerMenuOption.ReadByPhoneNumber, "Find customer by phone number"),
                new(ReadCustomerMenuOption.ReadByBookingId, "Find customer by booking ID"),
                new(ReadCustomerMenuOption.Back, "Back")
            });
        }
        internal ReadCustomerMenuOption Show()
        {
            var result = _menu.Show();
            if (result.WasCancelled)
                return ReadCustomerMenuOption.Back;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
