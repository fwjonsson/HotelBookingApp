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
            int? extraBeds = null;

            while (true)
            {
                var choice = _createRoomMenu.Show(roomNumber, roomType, extraBeds);
                switch (choice)
                {
                    case RoomEditorMenuOption.RoomNumber:
                        roomNumber = RoomFlowHelpers.ValidateRoomNumber();
                        break;

                    case RoomEditorMenuOption.RoomType:
                        (roomType, extraBeds) = RoomFlowHelpers.ValidateRoomType(roomType, extraBeds);
                        break;

                    case RoomEditorMenuOption.ExtraBeds:
                        extraBeds = RoomFlowHelpers.ValidateExtraBeds(extraBeds, roomType);
                        break;

                    case RoomEditorMenuOption.Save:
                        (bool saved, Room? createdRoom) = TrySave(roomNumber, roomType, extraBeds);
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

        private (bool, Room?) TrySave(int? roomNumber, RoomType? roomType, int? extraBeds)
        {
            if (roomNumber is not int rNumber ||
                roomType is not RoomType rType ||
                extraBeds is not int eBeds)
            {
                Console.WriteLine("You must fill in all values before creating.");
                InputHelper.WaitForContinue();
                return (false, null);
            }
            (RoomValidationResult result, Room? createdRoom) = _roomService.CreateRoom(rNumber, rType, eBeds);
            RoomValidationResultPrinter.PrintErrorsOrSuccess(result, "Room created.");
            InputHelper.WaitForContinue();
            if (result.Success)
                return (true, createdRoom);
            return (false, null);
        }
    }
}
