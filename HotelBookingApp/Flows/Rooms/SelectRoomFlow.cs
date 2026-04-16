using HotelBookingApp.Enums;
using HotelBookingApp.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.Printers.Rooms;

namespace HotelBookingApp.Flows.Rooms
{
    internal class SelectRoomFlow
    {
        private readonly RoomService _roomService;
        private readonly SelectObjectMenu<Room> _selectRoomMenu;

        internal SelectRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
            _selectRoomMenu = new SelectObjectMenu<Room>();
        }
        private Room? SelectRoom(Func<List<Room>> getRooms, Func<Room, string> formatter, string title)
        {
            while (true)
            {
                List<Room> rooms = getRooms();
                if (rooms.Count == 0)
                {
                    Console.WriteLine("No rooms found");
                    InputHelper.WaitForContinue();
                    return null;
                }
                Room? room = _selectRoomMenu.Show(rooms, formatter, title);
                if (room == null)
                {
                    _selectRoomMenu.ResetMenu();
                    return null;
                }
                _selectRoomMenu.ResetMenu();
                return room;
            }
        }
        internal Room? ReadAllAvailableAndGetSelected(DateOnly startDate, DateOnly endDate)
        {
            return SelectRoom(() => _roomService.ListAllAvailable(startDate, endDate), RoomPrinter.MenuShowAllFormatRoom,
                "--All Rooms--\n" +
                "     ID | Room Number | Extra Beds | Room Type\n" + new string('-', 47));
        }
        internal Room? ReadByRoomNumberAndGetIfAvailable(DateOnly startDate, DateOnly endDate)
        {
            int? roomNumberInput = InputHelper.ReadInt("Room number: ");
            if (roomNumberInput is not int roomNumber)
                return null;
            var checkRoomExists = _roomService.GetByRoomNumber(roomNumber);
            if (checkRoomExists is null)
            {
                Console.WriteLine("Room not found.");
                InputHelper.WaitForContinue();
                return null;
            }

            var room = _roomService.GetByRoomNumberAndIfAvailable(roomNumber, startDate, endDate);
            if (room is null)
            {
                Console.WriteLine("Room not available.");
                InputHelper.WaitForContinue();
            }
            return room;
        }
        internal Room? ReadAllAvailableByRoomTypeAndGetSelected(DateOnly startDate, DateOnly endDate)
        {
            RoomType? roomType = null;
            while (true)
            {
                int? roomTypeInput = InputHelper.ReadInt("Single or double room (Enter 1 or 2): ");
                if (roomTypeInput is not int input)
                    return null;

                switch (input)
                {
                    case 1:
                        roomType = RoomType.Single;
                        break;

                    case 2:
                        roomType = RoomType.Double;
                        break;

                    default:
                        Console.WriteLine("Must be 1 or 2");
                        InputHelper.WaitForContinue();
                        continue; ;
                }
                return SelectRoom(() => _roomService.GetByRoomTypeAndAvailable(roomType, startDate, endDate), RoomPrinter.MenuShowAllFormatRoom,
                    $"--All {roomType} Rooms--\n" +
                    "     ID | Room Number | Extra Beds | Room Type\n" + new string('-', 47));
            }
        }
    }
}
