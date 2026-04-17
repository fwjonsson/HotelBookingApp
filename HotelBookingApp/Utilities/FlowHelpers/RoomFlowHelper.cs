using HotelBookingApp.Enums;

namespace HotelBookingApp.Utilities.FlowHelpers
{
    internal static class RoomFlowHelper
    {
        internal static int? ValidateRoomNumber(int? roomNumber)
        {
            while (true)
            {
                int? input = InputHelper.ReadInt("Room number: ");
                if (input is not int)
                    return roomNumber;
                if (input < 1)
                {
                    Console.WriteLine("Must be greater than 0");
                    InputHelper.WaitForContinue();
                    continue;
                }
                else return input;
            }
        }
        internal static int? ValidateRoomSizeSqm2(int? roomSizeSqM2)
        {
            while (true)
            {
                int? input = InputHelper.ReadInt("Room Size In SqM2: ");
                if (input is not int)
                    return roomSizeSqM2;
                if (input < 1)
                {
                    Console.WriteLine("Must be greater than 0");
                    InputHelper.WaitForContinue();
                    continue;
                }
                else return input;
            }
        }

        internal static int? ValidateExtraBeds(int? extraBeds, RoomType? roomType, int? roomSizeSqm2)
        {
            if (roomType == null)
            {
                Console.WriteLine("Select a room type first.");
                InputHelper.WaitForContinue();
                return null;
            }
            if (roomSizeSqm2 is null)
            {
                Console.WriteLine("Enter room size first.");
                InputHelper.WaitForContinue();
                return null;
            }
            if (roomType == RoomType.Single || roomSizeSqm2 < 25)
            {
                Console.WriteLine("Extra beds only allowed in double rooms which are 25 sqm2 or larger.");
                InputHelper.WaitForContinue();
                return 0;
            }
            int? input = null;
            while (true)
            {
                if (roomSizeSqm2 < 30.0f)
                    input = InputHelper.ReadInt(extraBeds is null ?
                        $"Max 1 Extra Bed.\nExtra Beds:" :
                        $"Max 1 Extra Bed.\n(Currently {extraBeds})\nExtra Beds: ");

                else
                    input = InputHelper.ReadInt(extraBeds is null ?
                        $"Max 2 Extra Bed.\nExtra Beds:" :
                        $"Max 2 Extra Bed.\n(Currently {extraBeds})\nExtra Beds: ");
                if (input is not int)
                    return extraBeds;

                switch (input)
                {
                    case 0:
                        return 0;
                    case 1:
                        return 1;

                    case 2:
                        if (roomSizeSqm2 < 30)
                        {
                            Console.WriteLine("Max 1 extra bed in this room.");
                            InputHelper.WaitForContinue();
                            continue;
                        }
                        return 2;
                    default:
                        Console.WriteLine("Must be between 0 and 2.");
                        InputHelper.WaitForContinue();
                        continue;
                }
            }
        }

        internal static RoomType? ValidateRoomType(RoomType? roomType)
        {
            while (true)
            {
                int? input = InputHelper.ReadInt("Single or double room (Enter 1 or 2): ");
                if (input is not int)
                    return roomType;

                switch (input)
                {
                    case 1:
                        return RoomType.Single;

                    case 2:
                        return RoomType.Double;

                    default:
                        Console.WriteLine("Must enter 1 or 2.");
                        break;
                }

            }
        }
    }
}
