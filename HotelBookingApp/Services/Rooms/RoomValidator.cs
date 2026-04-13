using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Services.Rooms
{
    internal static class RoomValidator
    {
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
                else
                {
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
            }

            if (extraBeds is int eBeds)
            {

                if (!ValidNrOfExtraBeds(eBeds, checkedRoomType))
                {
                    result.ExtraBedsValid = false;
                }

                else
                    checkedExtraBeds = eBeds;
            }

            return (checkedRoomNumber, checkedRoomType, checkedExtraBeds, result);

        }
        private static bool ValidRoomNumber(int? roomNumber)
        {
            if (roomNumber < 1)
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
