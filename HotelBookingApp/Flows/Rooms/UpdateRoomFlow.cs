using HotelBookingApp.Enums;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Rooms
{
    internal class UpdateRoomFlow
    {
        private readonly RoomService _roomService;
        private readonly UpdateRoomMenu _updateRoomMenu;


        internal UpdateRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
            _updateRoomMenu = new UpdateRoomMenu();
        }

        internal void Run(int roomId)
        {
            Room? room = _roomService.GetById(roomId);
            if (!FlowHelper.CheckItemExistsAndWait(room, "Room not found."))
                return;
            int roomNr = room!.RoomNumber;
            RoomType roomType = room.RoomType;
            int extraBeds = room.ExtraBeds;
        }
    }
}
