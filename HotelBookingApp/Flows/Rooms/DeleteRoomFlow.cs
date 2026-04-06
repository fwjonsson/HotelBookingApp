using HotelBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Rooms
{
    internal class DeleteRoomFlow
    {
        private readonly RoomService _roomService;


        internal DeleteRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
        }
    }
}
