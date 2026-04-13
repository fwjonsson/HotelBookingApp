using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Rooms
{
    internal class DeleteRoomFlow
    {
        private readonly RoomService _roomService;


        internal DeleteRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
        }

        internal void Run(int roomId)
        {
            Room? room = _roomService.GetById(roomId);
            if (!FlowHelper.CheckItemExistsAndWait(room, "Room not found."))
                return;

            Console.WriteLine(RoomPrinter.FormatRoom(room!));

            if(InputHelper.WaitForYesOrNo("Are you sure you want to delete? Y/N"))
            {
                RoomValidationResult result = _roomService.DeleteRoom(room!.Id);

                if(!result.RoomFound)
                    Console.WriteLine("No room with that ID found.");
                if(!result.CanBeDeleted)
                    Console.WriteLine("Can not delete a room with current or future bookings.");
                if(result.Success)
                    Console.WriteLine("Room deleted.");
            }
            InputHelper.WaitForContinue();
        }
    }
}
