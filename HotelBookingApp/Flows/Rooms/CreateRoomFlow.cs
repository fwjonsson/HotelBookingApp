using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;

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

        internal void Run()
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
                        roomNumber = RoomFlowHelpers.ValidateRoomNumber(roomNumber);
                        break;
                    case RoomEditorMenuOption.RoomType:
                        (roomType, extraBeds) = RoomFlowHelpers.ValidateRoomType(roomType, extraBeds);
                        break;
                    case RoomEditorMenuOption.ExtraBeds:
                        extraBeds = RoomFlowHelpers. ValidateExtraBeds(extraBeds, roomType);
                        break;
                    case RoomEditorMenuOption.Save:
                        bool saved = TrySave(roomNumber, roomType, extraBeds);
                        InputHelper.WaitForContinue();
                        if (saved)
                        {
                            _createRoomMenu.ResetIndex();
                            return;
                        }
                        break;
                    case RoomEditorMenuOption.Cancel:
                        _createRoomMenu.ResetIndex();
                        return;
                }
            }
        }

        private bool TrySave(int? roomNumber, RoomType? roomType, int? extraBeds)
        {
            if (roomNumber is not int rNumber ||
                roomType is not RoomType rType ||
                extraBeds is not int eBeds)
            {
                Console.WriteLine("You must fill in all values before creating.");
                return false;
            }
            RoomValidationResult result = _roomService.CreateRoom(rNumber, rType, eBeds);
            if (!result.RoomNumberAvailable)
                Console.WriteLine("Room number unavailable.");
            if (!result.RoomTypeValid)
                Console.WriteLine("Invalid room type, has to be single or double.");
            if (!result.ExtraBedsValid)
                Console.WriteLine("Invalid amount of extra beds, max 1 for single room and max 2 for double room.");
            if (result.Success)
            {
                Console.WriteLine("Room created successfully.");
                return true;
            }
            return false;
        }
    }
}
