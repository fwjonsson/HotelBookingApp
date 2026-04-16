using HotelBookingApp.Data;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Services.Bookings
{

    internal class BookingService
    {
        private readonly ApplicationDbContext _dbContext;

        internal BookingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal BookingValidationResult CreateBooking(int customerId, int roomId, DateOnly startDate, DateOnly endDate)
        {
            var result = new BookingValidationResult();

            if (endDate <= startDate)
                result.EndDateIsAfterStartDate = false;

            if (startDate < DateOnly.FromDateTime(DateTime.Today))
                result.ValidDateRange = false;

            if (!CustomerExists(customerId))
                result.CustomerFound = false;

            if (!RoomExists(roomId))
                result.RoomFound = false;

            if (!IsRoomAvailable(roomId, startDate, endDate))
                result.RoomAvailable = false;

            if (!result.Success)
                return result;

            Booking newBooking = new Booking
            {
                CustomerId = customerId,
                RoomId = roomId,
                StartDate = startDate,
                EndDate = endDate
            };

            _dbContext.Bookings.Add(newBooking);
            _dbContext.SaveChanges();

            return result;
        }

        internal BookingValidationResult UpdateBooking(int bookingId, int newRoomId, int newCustomerId, DateOnly newStartDate, DateOnly newEndDate)
        {
            Booking? booking = _dbContext.Bookings.
                FirstOrDefault(b => b.Id == bookingId);

            var result = new BookingValidationResult();

            if (booking == null)
            {
                result.BookingFound = false;
                return result;
            }

            if (newEndDate <= newStartDate)
                result.EndDateIsAfterStartDate = false;

            if (newStartDate < DateOnly.FromDateTime(DateTime.Today))
                result.ValidDateRange = false;

            if (!CustomerExists(newCustomerId))
                result.CustomerFound = false;

            if (!RoomExists(newRoomId))
                result.RoomFound = false;

            if (!IsRoomAvailable(newRoomId, newStartDate, newEndDate, bookingId))
                result.RoomAvailable = false;

            if (!result.Success)
                return result;

            booking!.StartDate = newStartDate;
            booking.EndDate = newEndDate;
            booking.RoomId = newRoomId;
            booking.CustomerId = newCustomerId;

            _dbContext.SaveChanges();
            return result;

        }
        internal BookingValidationResult DeleteBooking(int bookingId)
        {
            Booking? booking = _dbContext.Bookings
                .FirstOrDefault(b => b.Id == bookingId);

            var result = new BookingValidationResult();

            if (booking == null)
            {
                result.BookingFound = false;
                return result;
            }

            _dbContext.Bookings.Remove(booking);
            _dbContext.SaveChanges();
            return result;
        }

        internal List<Booking> ListAll()
        {
            return _dbContext.Bookings
                .ToList();
        }

        internal List<Booking> GetByDate(DateOnly date)
        {
            return _dbContext.Bookings
                .Where(b => b.StartDate <= date && b.EndDate > date)
                .ToList();
        }

        internal Booking? GetById(int bookingId)
        {
            return _dbContext.Bookings
                .Include(b => b.Room)
                .FirstOrDefault(b => b.Id == bookingId);
        }

        internal List<Booking> GetByRoomId(int roomId)
        {
            return _dbContext.Bookings
                .Where(b => b.RoomId == roomId)
                .ToList();
        }

        internal List<Booking> GetByCustomerId(int customerId)
        {
            return _dbContext.Bookings
                .Where(b => b.CustomerId == customerId)
                .ToList();
        }

        private bool IsRoomAvailable(int roomId, DateOnly startDate, DateOnly endDate, int? bookingIdToExclude = null)
        {
            return !_dbContext.Bookings
                .Any(b =>
                (bookingIdToExclude == null || b.Id != bookingIdToExclude) &&
                b.RoomId == roomId &&
                startDate < b.EndDate &&
                b.StartDate < endDate);
        }
        private bool CustomerExists(int customerId)
        {
            return _dbContext.Customers
                .Any(c => c.Id == customerId);
        }
        private bool RoomExists(int roomId)
        {
            return _dbContext.Rooms
                .Any(r => r.Id == roomId);
        }
    }
}
