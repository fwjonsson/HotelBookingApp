using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Enums
{
    internal enum BookingResult
    {
        Success,
        BookingNotFound,
        InvalidDateRange,
        RoomUnavailable,
        RoomNotFound,
        CustomerNotFound,
        Fail
    }
}
