using HotelBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Rooms
{
    internal class ReadRoomFlow
    {
        private readonly RoomService _roomService;


        internal ReadRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
        }
    }
}
