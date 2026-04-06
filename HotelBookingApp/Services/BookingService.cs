using HotelBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Services
{

    internal class BookingService
    {
        private readonly ApplicationDbContext _dbContext;

        internal BookingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        internal void CreateBooking()
        {

        }
        internal void ReadBooking()
        {

        }
        internal void UpdateBooking()
        {

        }
        internal void DeleteBooking()
        {

        }
    }
}
