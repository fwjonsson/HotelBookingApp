using HotelBookingApp.Services.Rooms;
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
