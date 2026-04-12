using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities
{
    internal class MenuResult<T>
    {
        public bool WasCancelled { get; }
        public T? Value { get; }

        private MenuResult(bool wasCancelled, T? value)
        {
            WasCancelled = wasCancelled;
            Value = value;
        }
        internal static MenuResult<T> Cancelled()
        {
            return new MenuResult<T>(true, default);
        }
        internal static MenuResult<T> Selected(T value)
        {
            return new MenuResult<T>(false, value);
        }
    }
}
