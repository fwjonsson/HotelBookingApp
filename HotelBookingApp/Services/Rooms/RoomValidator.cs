using HotelBookingApp.Enums;
using HotelBookingApp.Models;

namespace HotelBookingApp.Services.Rooms
{
    internal static class RoomValidator
    {
        internal static (int?, RoomType?, int?, RoomValidationResult) ValidateRoomInput(int? roomNumber, int? roomTypeInput, int? extraBeds, RoomService roomService)
        {
            var result = new RoomValidationResult();
            int? checkedRoomNumber = null;
            RoomType? checkedRoomType = null;
            int? checkedExtraBeds = null;



            if (roomNumber is int rNumber)
            {
                if (!ValidRoomNumber(rNumber))
                    result.RoomNumberValid = false;
                else
                {
                    Room? roomNumberCheck = roomService.GetByRoomNr(rNumber);
                    if (roomNumberCheck != null)
                        result.RoomNumberAvailable = false;

                    if (result.RoomNumberValid && result.RoomNumberAvailable)
                        checkedRoomNumber = rNumber;
                }
            }


            if (roomTypeInput is int rTypeInput)
            {
                if (!ValidRoomType(rTypeInput))
                    result.RoomTypeValid = false;
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
                if (checkedRoomType == null)
                    result.ChoseRoomTypeBeforeExtraBeds = false;

                else if (!ValidNrOfExtraBeds(eBeds, checkedRoomType))
                    result.ExtraBedsValid = false;

                else
                    checkedExtraBeds = eBeds;
            }


            return (checkedRoomNumber, checkedRoomType, checkedExtraBeds, result);

        }
        private static bool ValidRoomNumber(int roomNumber)
        {
            if (roomNumber < 1)
            {
                return false;
            }
            else return true;
        }
        private static bool ValidNrOfExtraBeds(int extraBeds, RoomType? roomType)
        {
            if (roomType == RoomType.Single && extraBeds > 1 ||
                roomType == RoomType.Double && extraBeds > 2 ||
                extraBeds < 0
                )
            {
                return false;
            }
            return true;
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
                    return false;
            }

        }
    }
}
