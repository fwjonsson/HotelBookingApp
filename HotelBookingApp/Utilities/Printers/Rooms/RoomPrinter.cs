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
                $"Room Size: {room.RoomSizeSqm2} Sqm2\n" +
                $"Extra Beds: {room.ExtraBeds}\n";

        }
        internal static string MenuFormatRoom(Room room)
        {
            return $"Room ID: {room.Id}\n" +
                $"  Room Nr: {room.RoomNumber}\n" +
                $"  Room Type: {room.RoomType} Room\n" +
                $"  Room Size: {room.RoomSizeSqm2} Sqm2\n" +
                $"  Extra Beds: {room.ExtraBeds}\n";
        }
        internal static string MenuShowAllFormatRoom(Room room)
        {
            return $"{room.Id,6} |" +
                $"{room.RoomNumber,12} |" +
                $"{room.RoomType.ToString(),10} |" +
                $"{room.RoomSizeSqm2,5} Sqm2 |" +
                $"{room.ExtraBeds,11}";
        }
    }
}
