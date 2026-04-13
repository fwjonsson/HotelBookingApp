using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities
{
    internal static class FlowHelper
    {
        internal static bool CheckItemExistsAndWait<T>(T? item, string notFoundMessage) where T : class
        {
            if (item == null)
            {
                Console.WriteLine(notFoundMessage);
                InputHelper.WaitForContinue();
                return false;
            }
            return true;
        }
        internal static (int?, RoomType?, int?, RoomValidationResult) ValidateRoomEdits(int? roomNumber, int? roomTypeInput, int? extraBeds, RoomService roomService)
        {
            var result = new RoomValidationResult();
            int? checkedRoomNumber = null;
            RoomType? checkedRoomType = null;
            int? checkedExtraBeds = null;



            if (roomNumber is int rNumber)
            {
                if (!ValidRoomNumber(rNumber))
                    result.RoomNumberValid = false;

                Room? roomNumberCheck = roomService.GetByRoomNr(rNumber);
                if (roomNumberCheck != null)
                    result.RoomNumberAvailable = false;

                if (result.RoomNumberValid && result.RoomNumberAvailable)
                    checkedRoomNumber = rNumber;
            }


            if (roomTypeInput is int rTypeInput)
            {
                if (!ValidRoomType(rTypeInput))
                {
                    result.RoomTypeValid = false;
                }

                switch (rTypeInput)
                {
                    case 1:
                        checkedRoomType = RoomType.Single;
                        break;
                    case 2:
                        checkedRoomType = RoomType.Double;
                        break;
                }
            }

            if (!ValidNrOfExtraBeds(extraBeds, checkedRoomType))
            {
                result.ExtraBedsValid = false;
            }

            else
                checkedExtraBeds = extraBeds;

            return (checkedRoomNumber, checkedRoomType, checkedExtraBeds, result);

        }
        private static bool ValidRoomNumber(int? roomNumber)
        {
            var input = InputHelper.ReadInt("Room number:");
            if (input < 1)
            {
                Console.WriteLine("Must be greater than 0");
                InputHelper.WaitForContinue();
                return false;
            }
            else return true;
        }
        private static bool ValidNrOfExtraBeds(int? extraBeds, RoomType? roomType)
        {
            if (roomType == null)
            {
                Console.WriteLine("Must choose room type first.");
                InputHelper.WaitForContinue();
                return false;
            }
            if (roomType == RoomType.Single && extraBeds > 1)
            {
                Console.WriteLine("Max 1.");
            }
            else if (roomType == RoomType.Double && extraBeds > 2)
            {
                Console.WriteLine("Max 2.");
            }
            else if (extraBeds < 0)
            {
                Console.WriteLine("Extra beds cannot be negative.");
            }
            else
            {
                return true;
            }
            InputHelper.WaitForContinue();
            return false;
        }

        private static bool ValidRoomType(int rTypeInput)
        {
            switch (rTypeInput)
            {
                case 1:
                    return true;

                case 2:
                    return true;

                default:
                    Console.WriteLine("Must be 1 or 2");
                    InputHelper.WaitForContinue();
                    return false;
            }

        }
    }
}

