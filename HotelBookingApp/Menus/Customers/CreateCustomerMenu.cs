using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{
    internal class CreateCustomerMenu
    {
        private readonly ConsoleMenu<EditCustomerMenuOption> _menu;

        internal CreateCustomerMenu()
        {
            _menu = new ConsoleMenu<EditCustomerMenuOption>("Create Customer", new List<MenuChoice<EditCustomerMenuOption>>());
        }
        internal EditCustomerMenuOption Show(string? fullName, DateOnly? birthDate, string? email, string? phoneNumber)
        {
            _menu.SetChoices(new List<MenuChoice<EditCustomerMenuOption>>
            {
                new(EditCustomerMenuOption.FullName, $"Full Name: {fullName?.ToString() ?? "Not set"}"),
                new(EditCustomerMenuOption.BirthDate, $"Birth date: {birthDate?.ToString("yyyy-MM-dd") ?? "Not set"}"),
                new(EditCustomerMenuOption.Email, $"Email: {email?.ToString() ?? "Not set"}"),
                new(EditCustomerMenuOption.PhoneNumber, $"Phone Number: {phoneNumber?.ToString() ?? "Not set"}"),
                new(EditCustomerMenuOption.Save, $"Save"),
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
