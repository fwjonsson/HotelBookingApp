using HotelBookingApp.Enums;
using HotelBookingApp.Models;

namespace HotelBookingApp.Utilities.Printers.Rooms
{
    internal static class RoomPrinter
    {
        internal static string FormatRoom(Room room)
        {
            if (room.RoomType == RoomType.Single)
                return $"Room ID: {room.Id}\n" +
                    $"Room Number: {room.RoomNumber}\n" +
                    $"Room Type: Single Room\n" +
                    $"Extra Beds: {room.ExtraBeds}\n";


            else return $"Room ID: {room.Id}\n" +
                    $"Room Number: {room.RoomNumber}\n" +
                    $"Room Type: Double Room\n" +
                    $"Extra Beds: {room.ExtraBeds}\n";
        }
        internal static string MenuFormatRoom(Room room)
        {
            if (room.RoomType == RoomType.Single)
                return $"Room ID: {room.Id}\n" +
                    $"  Room Number: {room.RoomNumber}\n" +
                    $"  Room Type: Single Room\n" +
                    $"  Extra Beds: {room.ExtraBeds}\n";


            else return $"Room ID: {room.Id}\n" +
                    $"  Room Number: {room.RoomNumber}\n" +
                    $"  Room Type: Double Room\n" +
                    $"  Extra Beds: {room.ExtraBeds}\n";
        }
    }
}
