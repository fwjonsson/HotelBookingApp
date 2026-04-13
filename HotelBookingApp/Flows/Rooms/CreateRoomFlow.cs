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

        //internal void Run()
        //{
        //    int? roomNumber = null;
        //    RoomType? roomType = null;
        //    int? extraBeds = null;

        //    while (true)
        //    {
        //        var choice = _createRoomMenu.Show(roomNumber, roomType, extraBeds);
        //        switch (choice)
        //        {
        //            case RoomEditorMenuOption.RoomNumber:
        //                roomNumber = ValidateRoomNumber(roomNumber);
        //                break;
        //            case RoomEditorMenuOption.RoomType:
        //                (roomType, extraBeds) = ValidateRoomType(roomType, extraBeds);
        //                break;
        //            case RoomEditorMenuOption.ExtraBeds:
        //                extraBeds = ValidateExtraBeds(extraBeds, roomType);
        //                break;
        //            case RoomEditorMenuOption.Save:
        //                bool saved = TrySave(roomNumber, roomType, extraBeds);
        //                if (saved)
        //                    return;
        //                break;
        //            case RoomEditorMenuOption.Cancel:
        //                _createRoomMenu.ResetIndex();
        //                return;
        //        }
        //    }
        //}
        internal void Run()
        {
            int? roomNumber = null;
            RoomType? roomType = null;
            int? extraBeds = null;

            int? roomNumberInput = null;
            int? roomTypeInput = null;
            int? extraBedsInput = null;

            int? checkedRoomNumber = null;
            RoomType? checkedRoomType = null;
            int? checkedExtraBeds = null;

            var result = new RoomValidationResult();
            while (true)
            {
                var choice = _createRoomMenu.Show(roomNumber, roomType, extraBeds);
                switch (choice)
                {
                    case RoomEditorMenuOption.RoomNumber:
                        roomNumberInput = InputHelper.ReadInt("Enter room number: ");
                        break;
                    case RoomEditorMenuOption.RoomType:
                        roomTypeInput = InputHelper.ReadInt("Single or double room (Enter 1 or 2): ");
                        break;
                    case RoomEditorMenuOption.ExtraBeds:
                        extraBedsInput = InputHelper.ReadInt("Amount of extra beds (Max 1 for single room, max 2 for double room.");
                        break;
                    case RoomEditorMenuOption.Save:
                        bool saved = TrySave(roomNumber, roomType, extraBeds);
                        if (saved)
                            return;
                        break;
                    case RoomEditorMenuOption.Cancel:
                        _createRoomMenu.ResetIndex();
                        return;
                }
                (checkedRoomNumber, checkedRoomType, checkedExtraBeds, result) = RoomValidator.ValidateRoomInput(roomNumberInput, roomTypeInput, extraBedsInput, _roomService);
                if (checkedRoomNumber != null) roomNumber = checkedRoomNumber;
                if (checkedRoomType != null) roomType = checkedRoomType;
                if (checkedExtraBeds != null) extraBeds = checkedExtraBeds;

                if(!result.RoomNumberValid) Console.WriteLine("Room number invalid.");
                if(!result.RoomNumberAvailable) Console.WriteLine("Room number unavailable.");
                if(!result.RoomTypeValid) Console.WriteLine("Room type invalid.");
                if(!result.ChoseRoomTypeBeforeExtraBeds) Console.WriteLine("Must choose room type before choosing extra beds.");
                if(!result.ExtraBedsValid) Console.WriteLine("Invalid amount of extra beds");
                InputHelper.WaitForContinue();
            }
        }

        private int? ValidateRoomNumber(int? roomNumber)
        {
            var input = InputHelper.ReadInt("Room number:");
            if (input < 1)
            {
                Console.WriteLine("Must be greater than 0");
                InputHelper.WaitForContinue();
                return roomNumber;
            }
            else return input;
        }

        private bool TrySave(int? roomNumber, RoomType? roomType, int? extraBeds)
        {
            if (roomNumber is not int rNumber ||
                roomType is not RoomType rType ||
                extraBeds is not int eBeds)
            {
                Console.WriteLine("You must fill in all values before creating.");
                InputHelper.WaitForContinue();
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
                InputHelper.WaitForContinue();
                _createRoomMenu.ResetIndex();
                return true;
            }
            InputHelper.WaitForContinue();
            return false;
        }

        private int? ValidateExtraBeds(int? extraBeds, RoomType? roomType)
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
            else if (roomType == RoomType.Double && input > 2)
            {
                Console.WriteLine("Max 2.");
            }
            else if (input < 0)
            {
                Console.WriteLine("Extra beds cannot be negative.");
            }
            else
            {
                return input;
            }
            InputHelper.WaitForContinue();
            return extraBeds;
        }

        private (RoomType?, int?) ValidateRoomType(RoomType? roomType, int? extraBeds)
        {
            var input = InputHelper.ReadInt("Single or double room (Enter 1 or 2): ");
            switch (input)
            {
                case 1:
                    if (extraBeds > 1)
                    {
                        Console.WriteLine("Too many extra beds for this room size.");
                        InputHelper.WaitForContinue();
                        extraBeds = 1;
                    }
                    return (RoomType.Single, extraBeds);
                case 2:
                    if (extraBeds > 2)
                    {
                        Console.WriteLine("Too many extra beds for this room size.");
                        InputHelper.WaitForContinue();
                        extraBeds = 2;
                    }
                    return (RoomType.Double, extraBeds);
                default:
                    Console.WriteLine("Must be 1 or 2");
                    InputHelper.WaitForContinue();
                    return (roomType, extraBeds);
            }
        }
    }
}
