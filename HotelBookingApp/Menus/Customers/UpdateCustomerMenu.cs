using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{
    internal class UpdateCustomerMenu
    {
        private readonly ConsoleMenu<EditCustomerMenuOption> _menu;

        internal UpdateCustomerMenu()
        {
            _menu = new ConsoleMenu<EditCustomerMenuOption>(
                "Update Customer"
                , new List<MenuChoice<EditCustomerMenuOption>>());
        }
        internal EditCustomerMenuOption Show(string fullName, DateOnly birthDate, string email, string phoneNumber)
        {
            _menu.SetChoices(new List<MenuChoice<EditCustomerMenuOption>>
            {
                new(EditCustomerMenuOption.FullName, $"Change full name: {fullName}"),
                new(EditCustomerMenuOption.BirthDate, $"Change birth date: {birthDate:yyyy-MM-dd}"),
                new(EditCustomerMenuOption.Email, $"Change email: {email}"),
                new(EditCustomerMenuOption.PhoneNumber, $"Change phone number: {phoneNumber}"),
                new(EditCustomerMenuOption.Save, "Save changes"),
                new(EditCustomerMenuOption.Cancel, "Cancel")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return EditCustomerMenuOption.Cancel;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
