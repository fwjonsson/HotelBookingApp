using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Rooms;
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
            if (!FlowHelper.CheckItemExists(room, "Room not found."))
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
                        newRoomNumber = RoomFlowHelpers.ValidateRoomNumber(newRoomNumber) ?? newRoomNumber;
                        break;
                    case RoomEditorMenuOption.RoomType:
                        var (tempNewRoomType, tempNewExtraBeds) = RoomFlowHelpers.ValidateRoomType(newRoomType, newExtraBeds);
                        newRoomType = tempNewRoomType ?? newRoomType;
                        newExtraBeds = tempNewExtraBeds ?? newExtraBeds;
                        break;
                    case RoomEditorMenuOption.ExtraBeds:
                        newExtraBeds = RoomFlowHelpers.ValidateExtraBeds(newExtraBeds, newRoomType) ?? newExtraBeds;
                        break;
                    case RoomEditorMenuOption.Save:
                        bool saved = TrySave(room.Id, newRoomNumber,newRoomType,newExtraBeds);
                        InputHelper.WaitForContinue();
                        if (saved)
                        {
                            _updateRoomMenu.ResetIndex();
                            return;
                        }
                        break;
                    case RoomEditorMenuOption.Cancel:
                        _updateRoomMenu.ResetIndex();
                        return;
                }
            }

        }
        private bool TrySave(int roomId, int newRoomNumber, RoomType newRoomType, int newExtraBeds)
        {
            RoomValidationResult result = _roomService.UpdateRoom(roomId, newRoomNumber, newRoomType, newExtraBeds);
            RoomValidationResultPrinter.PrintErrorsOrSuccess(result, "Changes saved.");
            InputHelper.WaitForContinue();
            if (result.Success)
                return true;
            return false;
        }
    }
}
