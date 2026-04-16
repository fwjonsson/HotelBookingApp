using HotelBookingApp.Models;

namespace HotelBookingApp.Utilities.Printers.Rooms
{
    internal static class RoomPrinter
    {
        internal static string FormatRoom(Room room)
        {
            return $"Room ID: {room.Id}\n" +
                $"Room Nr: {room.RoomNumber}\n" +
                $"Room Type: {room.RoomType} Room\n" +
                $"Extra Beds: {room.ExtraBeds}\n";

        }
        internal static string MenuFormatRoom(Room room)
        {
            return $"Room ID: {room.Id}\n" +
                $"  Room Nr: {room.RoomNumber}\n" +
                $"  Room Type: {room.RoomType} Room\n" +
                $"  Extra Beds: {room.ExtraBeds}\n";
        }
        internal static string MenuShowAllFormatRoom(Room room)
        {
            return $"{room.Id,6} |" +
                $"{room.RoomNumber,12} |" +
                $"{room.ExtraBeds,11} |" +
                $"{room.RoomType.ToString(),10}";
        }
    }
}
