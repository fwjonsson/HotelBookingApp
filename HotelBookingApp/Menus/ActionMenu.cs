using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus
{
    internal class ActionMenu<T>
    {
        private readonly ConsoleMenu<ActionMenuOption> _menu;
        private readonly string _type;
        private readonly Func<T, string> _formatter;

        internal ActionMenu(string entityName, Func<T,string> formatter)
        {
            _type = entityName;
            _formatter = formatter;
            _menu = new ConsoleMenu<ActionMenuOption>("", new List<MenuChoice<ActionMenuOption>>());
        }
        internal ActionMenuOption Show(T type)
        {
            _menu.SetTitle($"{_type}\n\n{_formatter(type)}");
            _menu.SetChoices(new List<MenuChoice<ActionMenuOption>>
            {
                new(ActionMenuOption.Edit, "Edit"),
                new(ActionMenuOption.Delete, "Delete"),
                new(ActionMenuOption.Back, "Back")
            });
            return _menu.Show();
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }
    }
}
