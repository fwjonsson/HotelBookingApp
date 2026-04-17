using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Rooms;

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
            int newRoomSizeSqM2 = room.RoomSizeSqm2;
            while (true)
            {
                RoomEditorMenuOption choice = _updateRoomMenu.Show(newRoomNumber, newRoomType, newRoomSizeSqM2, newExtraBeds);

                switch (choice)
                {
                    case RoomEditorMenuOption.RoomNumber:
                        var newRoomNumberInput = RoomFlowHelper.ValidateRoomNumber(newRoomNumber);
                        if (newRoomNumberInput is int nRoomNumberInput)
                            newRoomNumber = nRoomNumberInput;
                        break;

                    case RoomEditorMenuOption.RoomType:
                        var newRoomTypeInput = RoomFlowHelper.ValidateRoomType(newRoomType);
                        if (newRoomTypeInput is RoomType nRoomTypeInput)
                        {
                            newRoomType = nRoomTypeInput;
                            newExtraBeds = 0;
                        }
                        break;

                    case RoomEditorMenuOption.RoomSizeSqm2:
                        var newRoomSizeSqM2Input = RoomFlowHelper.ValidateRoomSizeSqm2(newRoomSizeSqM2);
                        if (newRoomSizeSqM2Input is int nRoomSizeSqM2Input)
                        {
                            newRoomSizeSqM2 = nRoomSizeSqM2Input;
                            newExtraBeds = 0;
                        }
                        break;

                    case RoomEditorMenuOption.ExtraBeds:
                        var newExtraBedsInput = RoomFlowHelper.ValidateExtraBeds(newExtraBeds, newRoomType, newRoomSizeSqM2);
                        if (newExtraBedsInput is int nExtraBedsInput)
                            newExtraBeds = nExtraBedsInput;
                        break;

                    case RoomEditorMenuOption.Save:
                        if (TrySave(room.Id, newRoomNumber, newRoomType, newRoomSizeSqM2, newExtraBeds))
                        {
                            _updateRoomMenu.ResetMenu();
                            return;
                        }
                        break;

                    case RoomEditorMenuOption.Cancel:
                        _updateRoomMenu.ResetMenu();
                        return;
                }
            }
        }
        private bool TrySave(int roomId, int newRoomNumber, RoomType newRoomType, int newRoomSizeSqM2, int newExtraBeds)
        {
            RoomValidationResult result = _roomService.UpdateRoom(roomId, newRoomNumber, newRoomType, newRoomSizeSqM2, newExtraBeds);
            RoomValidationResultPrinter.PrintErrorsOrSuccess(result, "Changes saved.");
            InputHelper.WaitForContinue();

            return result.Success;
        }
    }
}
