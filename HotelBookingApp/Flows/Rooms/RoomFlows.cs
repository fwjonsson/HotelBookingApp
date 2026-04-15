using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;

namespace HotelBookingApp.Flows.Rooms
{
    internal class RoomFlows
    {
        private readonly RoomMenu _roomMenu;

        private readonly CreateRoomFlow _create;
        private readonly ReadRoomFlow _read;
        private readonly SelectRoomFlow _select;

        internal RoomFlows(RoomService roomService)
        {
            _roomMenu = new RoomMenu();

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
        internal Room? RunAndGetRoom()
        {
            Room? selectedRoom = null;
            while (true)
            {
                var choice = _roomMenu.Show();

                switch (choice)
                {
                    case RoomMenuOption.Create:
                        selectedRoom = _create.Run();
                        break;

                    case RoomMenuOption.ShowAll:
                        selectedRoom = _select.ReadAllAndGetSelected();
                        break;

                    case RoomMenuOption.FindByRoomNR:
                        selectedRoom = _select.ReadByRoomNumberAndGet();
                        break;

                    case RoomMenuOption.FindByRoomType:
                        selectedRoom = _select.ReadByRoomTypeAndGetSelected();
                        break;

                    case RoomMenuOption.FindByID:
                        selectedRoom = _select.ReadByIdAndGet();
                        break;

                    case RoomMenuOption.FindByCustomerID:
                        selectedRoom = _select.ReadByCustomerIdAndGetSelected();
                        break;

                    case RoomMenuOption.FindByBookingID:
                        selectedRoom = _select.ReadByBookingIdAndGet();
                        break;

                    case RoomMenuOption.Back:
                        _roomMenu.ResetMenu();
                        return null;
                }
                if (selectedRoom is null)
                    continue;

                _roomMenu.ResetMenu();
                return selectedRoom;
            }
        }
    }
}
