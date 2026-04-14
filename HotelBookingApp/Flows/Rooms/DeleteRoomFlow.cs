using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.Printers.Rooms;

namespace HotelBookingApp.Flows.Rooms
{
    internal class DeleteRoomFlow
    {
        private readonly RoomService _roomService;


        internal DeleteRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
        }

        internal bool TryDelete(int roomId)
        {
            if (InputHelper.WaitForYesOrNo("Are you sure you want to delete? Y/N"))
            {
                Room? room = _roomService.GetById(roomId);
                RoomValidationResult result = _roomService.DeleteRoom(room!.Id);
                RoomValidationResultPrinter.PrintErrorsOrSuccess(result, "Room deleted.");
                InputHelper.WaitForContinue();
                if (result.Success)
                    return true;
            }
            return false;
        }
    }
}
