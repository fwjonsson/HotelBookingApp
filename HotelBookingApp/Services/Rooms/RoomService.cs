using HotelBookingApp.Data;
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
    }
}
