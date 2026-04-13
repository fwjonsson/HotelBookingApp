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
                "Update Customer"
                , new List<MenuChoice<CustomerEditorMenuOption>>());
        }
        internal CustomerEditorMenuOption Show(string fullName, DateOnly birthDate, string email, string phoneNumber)
        {
            _menu.SetChoices(new List<MenuChoice<CustomerEditorMenuOption>>
            {
                new(CustomerEditorMenuOption.FullName, $"Change full name: {fullName}"),
                new(CustomerEditorMenuOption.BirthDate, $"Change birth date: {birthDate:yyyy-MM-dd}"),
                new(CustomerEditorMenuOption.Email, $"Change email: {email}"),
                new(CustomerEditorMenuOption.PhoneNumber, $"Change phone number: {phoneNumber}"),
                new(CustomerEditorMenuOption.Save, "Save changes"),
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
