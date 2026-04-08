using HotelBookingApp.Data;
using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;
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

        internal BookingResult CreateBooking(int customerId, int roomId, DateOnly startDate, DateOnly endDate)
        {
            if (endDate <= startDate)
                return BookingResult.InvalidDateRange;

            if (!CustomerExists(customerId))
                return BookingResult.CustomerNotFound;

            if (!RoomExists(roomId))
                return BookingResult.RoomNotFound;

            if (!IsRoomAvailable(roomId, startDate, endDate))
                return BookingResult.RoomUnavailable;

            Booking booking = new Booking
            {
                CustomerId = customerId,
                RoomId = roomId,
                StartDate = startDate,
                EndDate = endDate
            };

            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();

            return BookingResult.Success;
        }

        internal BookingResult UpdateBooking(int bookingId, DateOnly newStartDate, DateOnly newEndDate, int newRoomId, int newCustomerId)
        {
            Booking? booking = _dbContext.Bookings.FirstOrDefault(b => b.Id == bookingId);

            if (booking == null)
                return BookingResult.BookingNotFound;

            if (newEndDate <= newStartDate)
                return BookingResult.InvalidDateRange;

            if (!CustomerExists(newCustomerId))
                return BookingResult.CustomerNotFound;

            if (!RoomExists(newRoomId))
                return BookingResult.RoomNotFound;

            if (!IsRoomAvailable(newRoomId, newStartDate, newEndDate, bookingId))
                return BookingResult.RoomUnavailable;

            booking.StartDate = newStartDate;
            booking.EndDate = newEndDate;
            booking.RoomId = newRoomId;
            booking.CustomerId = newCustomerId;

            _dbContext.SaveChanges();
            return BookingResult.Success;

        }
        internal BookingResult DeleteBooking(int bookingId)
        {
            Booking? booking = _dbContext.Bookings.FirstOrDefault(b => b.Id == bookingId);

            if (booking == null)
                return BookingResult.BookingNotFound;

            _dbContext.Bookings.Remove(booking);
            _dbContext.SaveChanges();
            return BookingResult.Success;
        }

        internal List<Booking>ListAll()
        {
            return dbContext.Bookings.ToList();
        }

        internal List<Booking> GetByDate(DateOnly date)
        {
            return _dbContext.Bookings.Where(b => b.StartDate <= date && b.EndDate > date).ToList();
        }

        internal Booking? GetById(int bookingId)
        {
            return _dbContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
        }

        internal List<Booking> GetByRoomId(int roomId)
        {
            return _dbContext.Bookings.Where(b => b.RoomId == roomId).ToList();
        }

        internal List<Booking> GetByCustomerId(int customerId)
        {
            return _dbContext.Bookings.Where(b => b.CustomerId == customerId).ToList();
        }

        private bool IsRoomAvailable(int roomId, DateOnly startDate, DateOnly endDate, int? bookingIdToExclude = null)
        {
            return !_dbContext.Bookings.Any(b =>
                (bookingIdToExclude == null || b.Id != bookingIdToExclude) &&
                b.RoomId == roomId &&
                startDate < b.EndDate &&
                b.StartDate < endDate);
        }
        private bool CustomerExists(int customerId)
        {
            return _dbContext.Customers.Any(c => c.Id == customerId);
        }
        private bool RoomExists(int roomId)
        {
            return _dbContext.Rooms.Any(r => r.Id == roomId);
        }
    }
}
