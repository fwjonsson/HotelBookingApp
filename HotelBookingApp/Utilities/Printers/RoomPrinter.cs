using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities.Printers
{
    internal static class RoomPrinter
    {
        internal static string FormatRoom(Room room)
        {
            if (room.RoomType == 2)
            return $"Room ID: {room.Id}" +
                $"Room Number: {room.RoomNumber}" +
                $"R
        }
    }
}
