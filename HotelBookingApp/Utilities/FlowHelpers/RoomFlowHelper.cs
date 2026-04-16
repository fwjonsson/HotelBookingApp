using HotelBookingApp.Enums;

namespace HotelBookingApp.Utilities.FlowHelpers
{
    internal static class RoomFlowHelper
    {
        internal static int? ValidateRoomNumber()
        {
            while (true)
            {
                int? input = InputHelper.ReadInt("Room number: ");
                if (input is not int)
                    return null;
                if (input < 1)
                {
                    Console.WriteLine("Must be greater than 0");
                    InputHelper.WaitForContinue();
                    continue;
                }
                else return input;
            }
        }

        internal static int? ValidateExtraBeds(int? extraBeds, RoomType? roomType)
        {
            if (roomType == null)
            {
                Console.WriteLine("Select a room type first.");
                InputHelper.WaitForContinue();
                return null;
            }
            int? input = null;
            while (true)
            {
                if (roomType == RoomType.Single)
                    input = InputHelper.ReadInt($"Current extra beds: Extra beds (Max 1): ");

                else
                    input = InputHelper.ReadInt($"Current extra beds: Extra beds (Max 2): ");
                if (input is not int)
                    return extraBeds;

                switch (input)
                {
                    case 0:
                        return 0;
                    case 1:
                        return 1;

                    case 2:
                        if (roomType == RoomType.Single)
                        {
                            Console.WriteLine("Max 1 extra bed in single rooms.");
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

        internal static (RoomType?, int?) ValidateRoomType(RoomType? roomType, int? extraBeds)
        {
            while (true)
            {
                int? input = InputHelper.ReadInt("Single or double room (Enter 1 or 2): ");
                if (input is not int)
                    return (roomType, extraBeds);

                switch (input)
                {
                    case 1:
                        if (extraBeds > 1)
                        {
                            Console.WriteLine("Too many extra beds for this room size.");
                            InputHelper.WaitForContinue();
                        }
                        return (RoomType.Single, null);

                    case 2:
                        if (extraBeds > 2)
                        {
                            Console.WriteLine("Too many extra beds for this room size.");
                            InputHelper.WaitForContinue();
                        }
                        return (RoomType.Double, null);

                    default:
                        Console.WriteLine("Must be 1 or 2.");
                        InputHelper.WaitForContinue();
                        continue;
                }
            }
        }
    }
}
