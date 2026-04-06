using HotelBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Rooms
{
    internal class CreateRoomFlow
    {
        private readonly RoomService _roomService;


        internal CreateRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
        }
    }
}
