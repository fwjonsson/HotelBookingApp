using HotelBookingApp.Enums.Menus.Rooms;
using HotelBookingApp.Menus.Rooms;
using HotelBookingApp.Services.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Rooms
{
    internal class RoomFlows
    {
        private readonly RoomMenu _roomMenu;

        private readonly CreateRoomFlow _create;
        private readonly ReadRoomFlow _read;

        internal RoomFlows(RoomService roomService, RoomMenu roomMenu)
        {
            _roomMenu = roomMenu;

            _create = new CreateRoomFlow(roomService);
            _read = new ReadRoomFlow(roomService);
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
                    case RoomMenuOption.FindByID:
                        _read.ReadById();
                        break;
                    case RoomMenuOption.FindByRoomNR:
                        _read.ReadByRoomNr();
                        break;
                    case RoomMenuOption.FindByRoomType:
                        _read.ReadByRoomType();
                        break;
                    case RoomMenuOption.FindByCustomerID:
                        _read.ReadByCustomerId();
                        break;
                    case RoomMenuOption.FindByBookingID:
                        _read.ReadByBookingId();
                        break;
                    case RoomMenuOption.Back:
                        _roomMenu.ResetIndex();
                        return;

                }
            }
        }
    }
}
