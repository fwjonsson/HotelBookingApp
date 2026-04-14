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
                return $"Room ID: {room.Id}\n" +
                    $"Room Number: {room.RoomNumber}\n" +
                    $"Room Type: Single Room\n" +
                    $"Extra Beds: {room.ExtraBeds}\n";


            else return $"Room ID: {room.Id}\n" +
                    $"Room Number: {room.RoomNumber}\n" +
                    $"Room Type: Double Room\n" +
                    $"Extra Beds: {room.ExtraBeds}\n";
        }
        internal static void PrintRoom(Room room)
        {
            Console.WriteLine(FormatRoom(room));
            Console.WriteLine();
        }
    }
}
