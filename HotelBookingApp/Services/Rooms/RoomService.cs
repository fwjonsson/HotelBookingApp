using HotelBookingApp.Data;
using HotelBookingApp.Enums;
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
    }
}
