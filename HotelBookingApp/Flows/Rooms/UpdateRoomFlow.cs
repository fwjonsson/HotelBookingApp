using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities.FlowHelpers;
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

            int newRoomNumber = room!.RoomNumber;
            RoomType newRoomType = room.RoomType;
            int newExtraBeds = room.ExtraBeds;

            while (true)
            {
                RoomEditorMenuOption choice = _updateRoomMenu.Show(newRoomNumber, newRoomType, newExtraBeds);

                switch (choice)
                {
                    case RoomEditorMenuOption.RoomNumber:

                        break;
                    case RoomEditorMenuOption.RoomType:

                        break;
                    case RoomEditorMenuOption.ExtraBeds:

                        break;
                    case RoomEditorMenuOption.Save:

                        break;
                    case RoomEditorMenuOption.Cancel:

                        return;
                }
            }
        }
    }
}
