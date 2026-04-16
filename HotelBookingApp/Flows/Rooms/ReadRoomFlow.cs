using HotelBookingApp.Enums;
using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Menus;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Bookings;
using HotelBookingApp.Utilities.Printers.Rooms;

namespace HotelBookingApp.Flows.Rooms
{
    internal class ReadRoomFlow
    {
        private readonly RoomService _roomService;
        private readonly SelectObjectMenu<Room> _selectRoomMenu;
        private readonly string _tableHeader =
            " Room ID |   Room Nr   | Extra Beds | Room Type\n" + new string('-', 47);

        private readonly RoomActionMenu<Room> _roomActionMenu;
        private readonly UpdateRoomFlow _updateRoomFlow;
        private readonly DeleteRoomFlow _deleteRoomFlow;

        internal ReadRoomFlow(RoomService roomService)
        {
            _roomService = roomService;
            _selectRoomMenu = new SelectObjectMenu<Room>();
            _roomActionMenu = new RoomActionMenu<Room>("--Room--", RoomPrinter.FormatRoom);
            _updateRoomFlow = new UpdateRoomFlow(roomService);
            _deleteRoomFlow = new DeleteRoomFlow(roomService);
        }
        private void SelectRoomAndChooseAction(Func<List<Room>> getRooms, Func<Room, string> formatter, string title)
        {
            while (true)
            {
                List<Room> rooms = getRooms();
                if (rooms.Count == 0)
                {
                    Console.WriteLine("No rooms found");
                    InputHelper.WaitForContinue();
                    return;
                }
                Room? room = _selectRoomMenu.Show(rooms, formatter, title);
                if (room == null)
                {
                    _selectRoomMenu.ResetMenu();
                    return;
                }
                ChooseAction(_roomService.GetById, room.Id);
            }
        }
        private void ChooseAction<TId>(Func<TId, Room?> getRoom, TId identifier)
        {
            while (true)
            {
                Room? room = getRoom(identifier);
                if (!FlowHelper.CheckItemExists(room, "Room not found."))
                    return;

                RoomActionMenuOption choice = _roomActionMenu.Show(room!);

                switch (choice)
                {
                    case RoomActionMenuOption.ShowBookings:
                        ReadAllBookingsOnRoom(room!.Id);
                        break;

                    case RoomActionMenuOption.Edit:
                        _updateRoomFlow.Run(room!.Id);
                        break;

                    case RoomActionMenuOption.Delete:
                        if (_deleteRoomFlow.TryDelete(room!.Id))
                        {
                            _roomActionMenu.ResetMenu();
                            return;
                        }
                        break;

                    case RoomActionMenuOption.Back:
                        _roomActionMenu.ResetMenu();
                        return;
                }
            }
        }
        internal void ReadAllBookingsOnRoom(int roomId)
        {
            List<Booking> allBookingsOnRoom = _roomService.GetAllBookingsOnRoom(roomId);
            if (allBookingsOnRoom.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                InputHelper.WaitForContinue();
                return;
            }

            Console.Clear();
            foreach (Booking booking in allBookingsOnRoom)
            {
                Console.WriteLine(BookingPrinter.FormatBooking(booking));
            }

            InputHelper.WaitForContinue();

        }

        internal void ReadAll()
        {
            SelectRoomAndChooseAction(_roomService.ListAll, RoomPrinter.MenuShowAllFormatRoom,
                "--All Rooms--\n" +
                _tableHeader);
        }
        internal void ReadById()
        {
            int? roomIdInput = InputHelper.ReadInt("Room ID: ");
            if (roomIdInput is not int roomId)
                return;
            ChooseAction(_roomService.GetById, roomId);
        }
        internal void ReadByRoomNumber()
        {
            int? roomNumberInput = InputHelper.ReadInt("Room number: ");
            if (roomNumberInput is not int roomNumber)
                return;
            ChooseAction(_roomService.GetByRoomNumber, roomNumber);
        }
        internal void ReadByRoomType()
        {
            RoomType? roomType = null;
            while (true)
            {
                int? roomTypeInput = InputHelper.ReadInt("Single or double room (Enter 1 or 2): ");
                if (roomTypeInput is not int input)
                    return;

                switch (input)
                {
                    case 1:
                        roomType = RoomType.Single;
                        break;

                    case 2:
                        roomType = RoomType.Double;
                        break;

                    default:
                        Console.WriteLine("Must be 1 or 2");
                        InputHelper.WaitForContinue();
                        continue; ;
                }
                SelectRoomAndChooseAction(() => _roomService.GetByRoomType(roomType), RoomPrinter.MenuShowAllFormatRoom,
                    $"--All {roomType} Rooms--\n" +
                    _tableHeader);
                return;
            }
        }
        internal void ReadByCustomerId()
        {
            int? customerIdInput = InputHelper.ReadInt("Customer ID: ");
            if (customerIdInput is not int customerId)
                return;
            SelectRoomAndChooseAction(() => _roomService.GetByCustomerId(customerId), RoomPrinter.MenuShowAllFormatRoom,
                $"--All Rooms Booked By Customer ID: {customerId}--\n" +
                _tableHeader);
        }
        internal void ReadByBookingId()
        {
            int? bookingIdInput = InputHelper.ReadInt("Booking ID: ");
            if (bookingIdInput is not int bookingId)
                return;
            ChooseAction(_roomService.GetByBookingId, bookingId);
        }
    }
}
