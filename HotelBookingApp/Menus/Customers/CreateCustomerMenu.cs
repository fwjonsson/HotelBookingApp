using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{
    internal class CreateCustomerMenu
    {
        private readonly ConsoleMenu<CustomerEditorMenuOption> _menu;

        internal CreateCustomerMenu()
        {
            _menu = new ConsoleMenu<CustomerEditorMenuOption>("Create Customer", new List<MenuChoice<CustomerEditorMenuOption>>());
        }
        internal CustomerEditorMenuOption Show(string? fullName, DateOnly? birthDate, string? email, string? phoneNumber)
        {
            _menu.SetChoices(new List<MenuChoice<CustomerEditorMenuOption>>
            {
                new(CustomerEditorMenuOption.FullName, $"Full Name: {fullName?.ToString() ?? "Not set"}"),
                new(CustomerEditorMenuOption.BirthDate, $"Birth date: {birthDate?.ToString("yyyy-MM-dd") ?? "Not set"}"),
                new(CustomerEditorMenuOption.Email, $"Email: {email?.ToString() ?? "Not set"}"),
                new(CustomerEditorMenuOption.PhoneNumber, $"Phone Number: {phoneNumber?.ToString() ?? "Not set"}"),
                new(CustomerEditorMenuOption.Save, $"Save"),
                new(CustomerEditorMenuOption.Cancel, "Cancel")
            });
            var result = _menu.Show();
            if (result.WasCancelled)
                return CustomerEditorMenuOption.Cancel;
            return result.Value;
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
