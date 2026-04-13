using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

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
                    case EditRoomMenuOption.RoomNumber:
                        roomNumber = InputHelper.ReadInt("Room number:");
                        continue;
                    case EditRoomMenuOption.RoomType:
                        roomType = ValidateRoomType();
                        continue;
                    case EditRoomMenuOption.ExtraBeds:
                        extraBeds = ValidateExtraBeds(roomType);
                        continue;
                    case EditRoomMenuOption.Save:
                        bool saved = TrySave(roomNumber, roomType, extraBeds);
                        InputHelper.WaitForContinue();
                        if (saved)
                            return;
                        break;
                    case EditRoomMenuOption.Cancel:
                        _createRoomMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForContinue();
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
                Console.WriteLine("Invalid room type, has to be single or double,");
            if (!result.ExtraBedsValid)
                Console.WriteLine("Invalid amount of extra beds, max 1 for single room and max 2 for double room.");
            if (result.Success)
            {
                Console.WriteLine("Room created successfully.");
                _createRoomMenu.ResetIndex();
                return true;
            }
            return false;
        }

        private int? ValidateExtraBeds(RoomType? roomType)
        {
            if (roomType == null)
            {
                Console.WriteLine("Must choose room type first.");
                InputHelper.WaitForContinue();
                return null;
            }
            var input = InputHelper.ReadInt("Number of extra beds (Max 1 in single room and max 2 in double room): ");
            if (roomType == RoomType.Single && input > 1)
            {
                Console.WriteLine("Max 1.");
            }
            if (roomType == RoomType.Double && input > 2)
            {
                Console.WriteLine("Max 2,");
            }
            if (input < 0)
            {
                Console.WriteLine("Extra beds cannot me < 0.");
            }
            else
            {
                return input;
            }
            InputHelper.WaitForContinue();
            return null;
        }

        private RoomType? ValidateRoomType()
        {
            var input = InputHelper.ReadInt("Single or double room (Enter 1 or 2): ");
            if (input == 1) return RoomType.Single;
            if (input == 2) return RoomType.Double;
            else
            {
                Console.WriteLine("Must be 1 or 2");
                InputHelper.WaitForContinue();
                return null;
            }

        }
    }
}
