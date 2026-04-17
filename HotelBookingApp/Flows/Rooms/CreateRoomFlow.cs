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
    internal class CreateRoomFlow
    {
        private readonly RoomService _roomService;
        private readonly CreateRoomMenu _createRoomMenu;


        internal CreateRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
            _createRoomMenu = new CreateRoomMenu();
        }

        internal Room? Run()
        {
            int? roomNumber = null;
            RoomType? roomType = null;
            int? roomSizeSqm2 = null;
            int? extraBeds = null;

            while (true)
            {
                var choice = _createRoomMenu.Show(roomNumber, roomType, roomSizeSqm2, extraBeds);
                switch (choice)
                {
                    case RoomEditorMenuOption.RoomNumber:
                        roomNumber = RoomFlowHelper.ValidateRoomNumber(roomNumber);
                        break;

                    case RoomEditorMenuOption.RoomType:
                        roomType = RoomFlowHelper.ValidateRoomType(roomType);
                        extraBeds = 0;
                        break;

                    case RoomEditorMenuOption.RoomSizeSqm2:
                        roomSizeSqm2 = RoomFlowHelper.ValidateRoomSizeSqm2(roomSizeSqm2);
                        extraBeds = 0;
                        break;

                    case RoomEditorMenuOption.ExtraBeds:
                        extraBeds = RoomFlowHelper.ValidateExtraBeds(extraBeds, roomType, roomSizeSqm2);
                        break;

                    case RoomEditorMenuOption.Save:
                        (bool saved, Room? createdRoom) = TrySave(roomNumber, roomType, roomSizeSqm2, extraBeds);
                        if (saved)
                        {
                            _createRoomMenu.ResetMenu();
                            return createdRoom;
                        }
                        break;

                    case RoomEditorMenuOption.Cancel:
                        _createRoomMenu.ResetMenu();
                        return null;
                }
            }
        }

        private (bool, Room?) TrySave(int? roomNumber, RoomType? roomType, int? roomSizeSqm2, int? extraBeds)
        {
            if (roomNumber is not int rNumber ||
                roomType is not RoomType rType ||
                roomSizeSqm2 is not int rSizeSqm2 ||
                extraBeds is not int eBeds)
            {
                Console.WriteLine("You must fill in all values before creating.");
                InputHelper.WaitForContinue();
                return (false, null);
            }
            (RoomValidationResult result, Room? createdRoom) = _roomService.CreateRoom(rNumber, rType, rSizeSqm2, eBeds);
            RoomValidationResultPrinter.PrintErrorsOrSuccess(result, "Room created.");
            InputHelper.WaitForContinue();
            if (result.Success)
                return (true, createdRoom);
            return (false, null);
        }
    }
}
