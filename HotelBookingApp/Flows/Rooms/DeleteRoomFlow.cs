using HotelBookingApp.Services.Rooms;
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

        internal void Run(int id)
        {
            throw new NotImplementedException();
        }
    }
}
