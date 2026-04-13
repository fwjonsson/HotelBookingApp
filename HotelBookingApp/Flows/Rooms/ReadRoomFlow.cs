using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Services.Rooms;
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
        private readonly UpdateCustomerFlow _updateCustomerFlow;
        private readonly DeleteCustomerFlow _deleteCustomerFlow;

        internal ReadRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
            _selectRoomMenu = new SelectObjectMenu<Room>("Room", RoomPrinter.FormatRoom);
            _roomActionMenu = new ActionMenu<Room>("Room", RoomPrinter.FormatRoom);
            _updateCustomerFlow = new UpdateCustomerFlow(roomService);
            _deleteCustomerFlow = new DeleteCustomerFlow(roomService);
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
