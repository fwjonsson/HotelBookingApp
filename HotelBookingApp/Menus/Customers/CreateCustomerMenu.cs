using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{
    internal class CreateCustomerMenu
    {
        private readonly ConsoleMenu<CreateCustomerMenuOption> _menu;

        internal CreateCustomerMenu()
        {
            _menu = new ConsoleMenu<CreateCustomerMenuOption>("Create Customer", new List<MenuChoice<CreateCustomerMenuOption>>());
        }
        internal CreateCustomerMenuOption Show(string? fullName, DateOnly? birthDate, string? email, string? phoneNumber)
        {
            _menu.SetChoices(new List<MenuChoice<CreateCustomerMenuOption>>
            {
                new(CreateCustomerMenuOption.FullName, $"Full Name: {fullName?.ToString() ?? "Not set"}"),
                new(CreateCustomerMenuOption.BirthDate, $"Birth date: {birthDate?.ToString("yyyy-MM-dd") ?? "Not set"}"),
                new(CreateCustomerMenuOption.Email, $"Email: {email?.ToString() ?? "Not set"}"),
                new(CreateCustomerMenuOption.PhoneNumber, $"Phone Number: {phoneNumber?.ToString() ?? "Not set"}"),
                new(CreateCustomerMenuOption.Create, $"Create Customer"),
                new(CreateCustomerMenuOption.Cancel, "Cancel")
            });
            return _menu.Show();
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
