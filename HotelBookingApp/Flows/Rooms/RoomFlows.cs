using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Rooms;

namespace HotelBookingApp.Flows.Rooms
{
    internal class RoomFlows
    {
        private readonly RoomMenu _roomMenu;
        private readonly AvailableRoomsMenu _availableRoomsMenu;
        private readonly RoomService _roomService;

        private readonly CreateRoomFlow _create;
        private readonly ReadRoomFlow _read;
        private readonly SelectRoomFlow _select;

        internal RoomFlows(RoomService roomService)
        {
            _roomMenu = new RoomMenu();
            _availableRoomsMenu = new AvailableRoomsMenu();
            _roomService = roomService;

            _create = new CreateRoomFlow(roomService);
            _read = new ReadRoomFlow(roomService);
            _select = new SelectRoomFlow(roomService);
        }
        internal void Run()
        {
            while (true)
            {
                var choice = _roomMenu.Show();

                switch (choice)
                {
                    case RoomMenuOption.Create:
                        _create.Run();
                        break;

                    case RoomMenuOption.ShowAll:
                        _read.ReadAll();
                        break;

                    case RoomMenuOption.FindByRoomNr:
                        _read.ReadByRoomNumber();
                        break;

                    case RoomMenuOption.FindByRoomType:
                        _read.ReadByRoomType();
                        break;

                    case RoomMenuOption.FindById:
                        _read.ReadById();
                        break;

                    case RoomMenuOption.FindByCustomerId:
                        _read.ReadByCustomerId();
                        break;

                    case RoomMenuOption.FindByBookingId:
                        _read.ReadByBookingId();
                        break;

                    case RoomMenuOption.Back:
                        _roomMenu.ResetMenu();
                        return;
                }
            }
        }
        internal Room? RunAndGetRoom(DateOnly startDate, DateOnly endDate, Room? room)
        {
            Room? selectedRoom = room;
            int? newExtraBedsInput = null;
            int newExtraBeds = 0;

            if (selectedRoom is not null)
                newExtraBeds = selectedRoom.ExtraBeds;
            
            while (true)
            {
                if (selectedRoom is not null)
                    room = selectedRoom;

                var choice = _availableRoomsMenu.Show(room, newExtraBeds);

                switch (choice)
                {
                    case RoomMenuOption.ShowAll:
                        selectedRoom = _select.ReadAllAvailableAndGetSelected(startDate, endDate);
                        break;

                    case RoomMenuOption.FindByRoomNr:
                        selectedRoom = _select.ReadByRoomNumberAndGetIfAvailable(startDate, endDate);
                        break;

                    case RoomMenuOption.FindByRoomType:
                        selectedRoom = _select.ReadAllAvailableByRoomTypeAndGetSelected(startDate, endDate);
                        break;

                    case RoomMenuOption.ChangeExtraBeds:
                        if (room is null)
                        {
                            Console.WriteLine("Select a room first.");
                            InputHelper.WaitForContinue();
                            break;
                        }
                        newExtraBedsInput = RoomFlowHelper.ValidateExtraBeds(newExtraBeds, room.RoomType);
                        if (newExtraBedsInput is not int nExtraBeds)
                            break;

                        newExtraBeds = nExtraBeds;
                        break;

                    case RoomMenuOption.Save:
                        if (room is not null)
                        {
                            RoomValidationResult result = _roomService.UpdateRoom(room.Id, room.RoomNumber, room.RoomType, newExtraBeds);
                            RoomValidationResultPrinter.PrintErrorsOrSuccess(result, "Room updated.");
                            InputHelper.WaitForContinue();
                            return room;
                        }
                        Console.WriteLine("Select room first.");
                        InputHelper.WaitForContinue();
                        break;

                    case RoomMenuOption.Back:
                        _availableRoomsMenu.ResetMenu();
                        return null;
                }

            }
        }

    }
}
