using HotelBookingApp.Data;
using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Services.Rooms
{
    internal class RoomService
    {
        private readonly ApplicationDbContext _dbContext;

        internal RoomService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal RoomValidationResult CreateRoom(int rNumber, RoomType rType, int eBeds)
        {
            throw new NotImplementedException();
        }

        internal Room? GetByBookingId(int arg)
        {
            throw new NotImplementedException();
        }

        internal List<Room> GetByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        internal Room? GetById(int roomId)
        {
            throw new NotImplementedException();
        }

        internal Room? GetByRoomNr(int arg)
        {
            throw new NotImplementedException();
        }

        internal List<Room> GetByRoomType(RoomType? roomType)
        {
            throw new NotImplementedException();
        }

        internal List<Room> ListAll()
        {
            throw new NotImplementedException();
        }

        internal RoomValidationResult UpdateRoom(int roomId, int newRoomNumber, RoomType newRoomType, int newExtraBeds)
        {
            throw new NotImplementedException();
        }
    }
}
