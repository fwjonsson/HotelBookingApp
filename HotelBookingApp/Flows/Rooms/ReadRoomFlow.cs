using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Rooms;

namespace HotelBookingApp.Flows.Rooms
{
    internal class ReadRoomFlow
    {
        private readonly RoomService _roomService;
        private readonly SelectObjectMenu<Room> _selectRoomMenu;
        private readonly ActionMenu<Room> _roomActionMenu;
        private readonly UpdateRoomFlow _updateRoomFlow;
        private readonly DeleteRoomFlow _deleteRoomFlow;

        internal ReadRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
            _selectRoomMenu = new SelectObjectMenu<Room>(RoomPrinter.MenuFormatRoom);
            _roomActionMenu = new ActionMenu<Room>("Room", RoomPrinter.MenuFormatRoom);
            _updateRoomFlow = new UpdateRoomFlow(roomService);
            _deleteRoomFlow = new DeleteRoomFlow(roomService);
        }
        private void SelectRoomAndChooseAction(Func<List<Room>> getRooms, string title)
        {
            while (true)
            {
                List<Room> rooms = getRooms();
                if (rooms.Count == 0)
                {
                    Console.WriteLine("No rooms found");
                    InputHelper.WaitForContinue();
                    return;
                }
                Room? room = _selectRoomMenu.Show(rooms, title);
                if (room == null)
                {
                    _selectRoomMenu.ResetIndex();
                    return;
                }
                ChooseAction(_roomService.GetById, room.Id);
            }
        }
        private void ChooseAction<TId>(Func<TId, Room?> getRoom, TId identifier)
        {
            while (true)
            {
                Room? room = getRoom(identifier);
                if (!FlowHelper.CheckItemExists(room, "Room not found"))
                    return;

                ActionMenuOption choice = _roomActionMenu.Show(room!);

                switch (choice)
                {
                    case ActionMenuOption.Edit:
                        _updateRoomFlow.Run(room!.Id);
                        break;
                    case ActionMenuOption.Delete:
                        if (_deleteRoomFlow.TryDelete(room!.Id))
                        {
                            _roomActionMenu.ResetIndex();
                            return;
                        }
                        break;
                    case ActionMenuOption.Back:
                        _roomActionMenu.ResetIndex();
                        return;
                }
            }
        }

        internal void ReadAll()
        {
            SelectRoomAndChooseAction(_roomService.ListAll, "--All Rooms--");
        }
        internal void ReadById()
        {
            var roomId = InputHelper.ReadInt("Room ID: ");
            ChooseAction(_roomService.GetById, roomId);
        }
        internal void ReadByRoomNumber()
        {
            var roomNumber = InputHelper.ReadInt("Room number: ");
            ChooseAction(_roomService.GetByRoomNumber, roomNumber);
        }
        internal void ReadByRoomType()
        {
            var input = InputHelper.ReadInt("Single or double room (Enter 1 or 2): ");
            RoomType? roomType = null;
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
                    return;
            }
            SelectRoomAndChooseAction(() => _roomService.GetByRoomType(roomType), $"--All {roomType} Rooms--");
        }
        internal void ReadByCustomerId()
        {
            var customerId = InputHelper.ReadInt("Customer ID: ");
            SelectRoomAndChooseAction(() => _roomService.GetByCustomerId(customerId), $"All Rooms By Customer ID: {customerId}--");
        }
        internal void ReadByBookingId()
        {
            var bookingId = InputHelper.ReadInt("Booking ID: ");
            ChooseAction(_roomService.GetByBookingId, bookingId);
        }
    }
}
