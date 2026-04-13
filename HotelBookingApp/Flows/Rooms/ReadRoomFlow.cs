using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Services.Rooms;
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

        internal void ReadAll()
        {
            throw new NotImplementedException();
        }

        internal void ReadByBookingId()
        {
            throw new NotImplementedException();
        }

        internal void ReadByCustomerId()
        {
            throw new NotImplementedException();
        }

        internal void ReadById()
        {
            throw new NotImplementedException();
        }

        internal void ReadByRoomNr()
        {
            throw new NotImplementedException();
        }

        internal void ReadByRoomSize()
        {
            throw new NotImplementedException();
        }
    }
}
