using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Rooms
{
    internal class RoomFlows
    {
        private readonly RoomMenu _roomMenu;

        private readonly CreateRoomFlow _create;
        private readonly ReadRoomFlow _read;
        private readonly UpdateRoomFlow _update;
        private readonly DeleteRoomFlow _delete;

        internal RoomFlows(RoomService roomService, RoomMenu roomMenu)
        {
            _roomMenu = roomMenu;

            _create = new CreateRoomFlow(roomService);
            _read = new ReadRoomFlow(roomService);
            _update = new UpdateRoomFlow(roomService);
            _delete = new DeleteRoomFlow(roomService);
        }
        internal void Run()
        {

        }
    }
}
