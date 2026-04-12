using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{
    internal class UpdateCustomerMenu
    {
        private readonly ConsoleMenu<UpdateCustomerMenuOption> _menu;

        internal UpdateCustomerMenu()
        {
            _menu = new ConsoleMenu<UpdateCustomerMenuOption>(
                "Update Customer"
                , new List<MenuChoice<UpdateCustomerMenuOption>>());
        }
        internal UpdateCustomerMenuOption Show(string fullName, DateOnly birthDate, string email, string phoneNumber)
        {
            _menu.SetChoices(new List<MenuChoice<UpdateCustomerMenuOption>>
            {
                new(UpdateCustomerMenuOption.FullName, $"Change full name: {fullName}"),
                new(UpdateCustomerMenuOption.BirthDate, $"Change birth date: {birthDate:yyyy-MM-dd}"),
                new(UpdateCustomerMenuOption.Email, $"Change email: {email}"),
                new(UpdateCustomerMenuOption.PhoneNumber, $"Change phone number: {phoneNumber}"),
                new(UpdateCustomerMenuOption.Save, "Save changes"),
                new(UpdateCustomerMenuOption.Cancel, "Cancel")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return UpdateCustomerMenuOption.Cancel;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
