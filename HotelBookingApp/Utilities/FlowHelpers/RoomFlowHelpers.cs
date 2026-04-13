using HotelBookingApp.Enums;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities.FlowHelpers
{
    internal static class RoomFlowHelpers
    {
        internal static int? ValidateRoomNumber(int? roomNumber)
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

        internal static int? ValidateExtraBeds(int? extraBeds, RoomType? roomType)
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

        internal static  (RoomType?, int?) ValidateRoomType(RoomType? roomType, int? extraBeds)
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
