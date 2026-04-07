using HotelBookingApp.Data;
using HotelBookingApp.Models;
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
        internal void UpdateBooking(Booking booking, DateOnly? newStartDate, DateOnly? newEndDate, int? newRoomId, int? newCustomerId)
        {
            
        }
        internal void DeleteBooking()
        {

        }

        internal List<Booking> ListAll()
        {
            throw new NotImplementedException();
        }

        internal List<Booking> GetByDate(DateOnly date)
        {
            throw new NotImplementedException();
        }

        internal Booking? GetById(int id)
        {
            throw new NotImplementedException();
        }

        internal List<Booking> GetByRoomId(int id)
        {
            throw new NotImplementedException();
        }

        internal List<Booking> GetByCustomerId(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
