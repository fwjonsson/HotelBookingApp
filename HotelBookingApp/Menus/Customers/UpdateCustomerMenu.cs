using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Utilities.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{
    internal class UpdateCustomerMenu
    {
        private readonly ConsoleMenu<CustomerEditorMenuOption> _menu;

        internal UpdateCustomerMenu()
        {
            _menu = new ConsoleMenu<CustomerEditorMenuOption>(
                "--Update Customer--"
                , new List<MenuChoice<CustomerEditorMenuOption>>());
        }
        internal CustomerEditorMenuOption Show(string fullName, DateOnly birthDate, string email, string phoneNumber)
        {
            _menu.SetChoices(new List<MenuChoice<CustomerEditorMenuOption>>
            {
                new(CustomerEditorMenuOption.FullName, $"Full Name: {fullName}"),
                new(CustomerEditorMenuOption.BirthDate, $"Birth Date: {birthDate:yyyy-MM-dd}"),
                new(CustomerEditorMenuOption.Email, $"Email: {email}"),
                new(CustomerEditorMenuOption.PhoneNumber, $"Phone Number: {phoneNumber}"),
                new(CustomerEditorMenuOption.Save, "Save"),
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
