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
            if (room.RoomType == Enums.RoomType.Single)
                return $"Room ID: {room.Id}" +
                    $"Room Number: {room.RoomNumber}" +
                    $"Room Type: Single Room" +
                    $"Extra Beds: {room.ExtraBeds}";


            else return $"Room ID: {room.Id}" +
                    $"Room Number: {room.RoomNumber}" +
                    $"Room Type: Double Room" +
                    $"Extra Beds: {room.ExtraBeds}";
        }
        internal static void PrintRoom(Room room)
        {
            Console.WriteLine(FormatRoom(room));
            Console.WriteLine();
        }
    }
}
