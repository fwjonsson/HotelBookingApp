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

                    case RoomMenuOption.FindByRoomNR:
                        _read.ReadByRoomNumber();
                        break;

                    case RoomMenuOption.FindByRoomType:
                        _read.ReadByRoomType();
                        break;

                    case RoomMenuOption.FindByID:
                        _read.ReadById();
                        break;

                    case RoomMenuOption.FindByCustomerID:
                        _read.ReadByCustomerId();
                        break;

                    case RoomMenuOption.FindByBookingID:
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
            while (true)
            {
                var choice = _availableRoomsMenu.Show();

                switch (choice)
                {
                    case RoomMenuOption.ShowAll:
                        room = _select.ReadAllAvailableAndGetSelected(startDate, endDate);
                        break;

                    case RoomMenuOption.FindByRoomNR:
                        room = _select.ReadByRoomNumberAndGetIfAvailable(startDate, endDate);
                        break;

                    case RoomMenuOption.FindByRoomType:
                        room = _select.ReadAllAvailableByRoomTypeAndGetSelected(startDate, endDate);
                        break;

                    case RoomMenuOption.ChangeExtraBeds:
                        if (room is null)
                        {
                            Console.WriteLine("Select a room first.");
                            InputHelper.WaitForContinue();
                            break;
                        }
                        int? ExtraBedsInput = RoomFlowHelper.ValidateExtraBeds(room.ExtraBeds, room.RoomType);
                        if (ExtraBedsInput is not int newExtraBeds)
                            break;

                        var result = _roomService.UpdateRoom(room.Id, room.RoomNumber, room.RoomType, newExtraBeds);
                        RoomValidationResultPrinter.PrintErrorsOrSuccess(result, "Room updated.");
                        InputHelper.WaitForContinue();
                        break;

                    case RoomMenuOption.Back:
                        _availableRoomsMenu.ResetMenu();
                        return null;
                }
                if (room is null)
                    continue;

                _availableRoomsMenu.ResetMenu();
                return room;
            }
        }

    }
}
