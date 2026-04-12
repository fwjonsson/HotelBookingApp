using HotelBookingApp.Models;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus.Customers
{
    internal class SelectCustomerMenu
    {
        private readonly ConsoleMenu<int> _menu;

        internal SelectCustomerMenu()
        {
            _menu = new ConsoleMenu<int>("Select Customer", new List<MenuChoice<int>>());
        }
        internal Customer? Show(List<Customer> customers)
        {
            if (customers.Count == 0)
                return null;

            var choices = new List<MenuChoice<int>>();

            for (int i = 0; i < customers.Count; i++)
            {
                Customer customer = customers[i];

                string text = $"{customer.Id} - {customer.FullName} - {customer.Email}";
                choices.Add(new MenuChoice<int>(i, text));
            }

            int backIndex = choices.Count;
            choices.Add(new MenuChoice<int>(backIndex, "Back"));

            _menu.SetChoices(choices);
            int selectedIndex = _menu.Show();
            if (selectedIndex == backIndex)
            {
                _menu.ResetIndex();
                return null;
            }
            return customers[selectedIndex];
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
