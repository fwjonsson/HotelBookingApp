using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.Printers;
using System;
using System.Collections.Generic;
using System.Text;

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
            _selectRoomMenu = new SelectObjectMenu<Room>("Room", RoomPrinter.FormatRoom);
            _roomActionMenu = new ActionMenu<Room>("Room", RoomPrinter.FormatRoom);
            _updateRoomFlow = new UpdateRoomFlow(roomService);
            _deleteRoomFlow = new DeleteRoomFlow(roomService);
        }
        private void SelectRoomAndChooseAction(Func<List<Room>> getRooms)
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
                Room? room = _selectRoomMenu.Show(rooms);
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
                if (!FlowHelper.CheckItemExistsAndWait(room, "Room not found"))
                    return;

                ActionMenuOption choice = _roomActionMenu.Show(room!);

                switch (choice)
                {
                    case ActionMenuOption.Edit:
                        _updateRoomFlow.Run(room!.Id);
                        break;
                    case ActionMenuOption.Delete:
                        _deleteRoomFlow.Run(room!.Id);
                        _roomActionMenu.ResetIndex();
                        return;
                    case ActionMenuOption.Back:
                        _roomActionMenu.ResetIndex();
                        return;
                }
            }
        }

        internal void ReadAll()
        {
            SelectRoomAndChooseAction(_roomService.ListAll);
        }
        internal void ReadById()
        {
            var roomId = InputHelper.ReadInt("Enter room ID: ");
            ChooseAction(_roomService.GetById, roomId);
        }
        internal void ReadByRoomNr()
        {
            var roomNr = InputHelper.ReadInt("Enter room number: ");
            ChooseAction(_roomService.GetByRoomNr, roomNr);
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
            SelectRoomAndChooseAction(() => _roomService.GetByRoomType(roomType));
        }
        internal void ReadByCustomerId()
        {
            var customerId = InputHelper.ReadInt("Enter customer ID: ");
            SelectRoomAndChooseAction(() => _roomService.GetByCustomerId(customerId));
        }
        internal void ReadByBookingId()
        {
            var bookingId = InputHelper.ReadInt("Enter booking ID: ");
            ChooseAction(_roomService.GetByBookingId, bookingId);
        }
    }
}
