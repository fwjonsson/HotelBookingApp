using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities.Menus
{
    internal class MenuChoice<T>
    {
        public T Value { get; }
        public string Text { get;  }

        internal MenuChoice(T value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
