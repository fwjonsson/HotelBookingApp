using HotelBookingApp.Models;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Menus
{
    internal class SelectObjectMenu<T> where T : class
    {
        private readonly ConsoleMenu<int> _menu;
        private readonly string _title;
        private readonly Func<T, string> _formatter;

        internal SelectObjectMenu(string item, Func<T, string> formatter)
        {
            _title = item;
            _menu = new ConsoleMenu<int>("", new List<MenuChoice<int>>());
            _formatter = formatter;
        }
        internal T? Show(List<T> itemList)
        {
            if (itemList.Count == 0)
                return null;
            _menu.SetTitle(_title);
            var choices = new List<MenuChoice<int>>();

            for (int i = 0; i < itemList.Count; i++)
            {
                T item = itemList[i];

                string text = _formatter(item);
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
            return itemList[selectedIndex];
        }
        internal void ResetIndex()
        {
            _menu.ResetIndex();
        }

        internal void CreateChoices(List<T> typeList)
        {

        }
    }
}
