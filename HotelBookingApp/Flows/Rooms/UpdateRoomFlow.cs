using HotelBookingApp.Services.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Rooms
{
    internal class UpdateRoomFlow
    {
        private readonly RoomService _roomService;


        internal UpdateRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
        }
    }
}
