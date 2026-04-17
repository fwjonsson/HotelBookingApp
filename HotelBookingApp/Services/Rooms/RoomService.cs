using HotelBookingApp.Data;
using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Services.Rooms
{
    internal class RoomService
    {
        private readonly ApplicationDbContext _dbContext;

        internal RoomService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal (RoomValidationResult, Room?) CreateRoom(int rNumber, RoomType rType, int rSizeSqm2, int eBeds)
        {
            var result = new RoomValidationResult();

            if (!RoomNumberAvailable(rNumber))
                result.RoomNumberAvailable = false;

            if (!ValidRoomNumber(rNumber))
                result.RoomNumberValid = false;

            if (!ValidRoomSize(rSizeSqm2))
                result.RoomSizeSqm2Valid = false;

            if (!ValidAmountOfExtraBeds(eBeds, rType, rSizeSqm2))
                result.ExtraBedsValid = false;

            if (!result.Success)
                return (result, null);

            var newRoom = new Room
            {
                RoomNumber = rNumber,
                RoomType = rType,
                RoomSizeSqm2 = rSizeSqm2,
                ExtraBeds = eBeds
            };
            _dbContext.Rooms
                .Add(newRoom);
            _dbContext.SaveChanges();
            return (result, newRoom);
        }
        internal RoomValidationResult UpdateRoom(int roomId, int newRoomNumber, RoomType newRoomType, int newRoomSizeSqm2, int newExtraBeds)
        {
            Room? room = _dbContext.Rooms
                .FirstOrDefault(r => r.Id == roomId);

            var result = new RoomValidationResult();

            if (room == null)
            {
                result.RoomFound = false;
                return result;
            }
            if (!RoomNumberAvailable(newRoomNumber, roomId))
                result.RoomNumberAvailable = false;

            if (!ValidRoomNumber(newRoomNumber))
                result.RoomNumberValid = false;

            if (!ValidRoomSize(newRoomSizeSqm2))
                result.RoomSizeSqm2Valid = false;

            if (!ValidAmountOfExtraBeds(newExtraBeds, newRoomType, newRoomSizeSqm2))
                result.ExtraBedsValid = false;

            if (!result.Success)
                return result;

            room!.RoomNumber = newRoomNumber;
            room.RoomType = newRoomType;
            room.RoomSizeSqm2 = newRoomSizeSqm2;
            room.ExtraBeds = newExtraBeds;

            _dbContext.SaveChanges();
            return result;
        }
        internal RoomValidationResult DeleteRoom(int roomId)
        {
            Room? room = _dbContext.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.Id == roomId);

            var result = new RoomValidationResult();

            if (room == null)
            {
                result.RoomFound = false;
                return result;
            }
            if (HasCurrentOrFutureBooking(roomId))
                result.CanBeDeleted = false;
            if (!result.Success)
                return result;

            _dbContext.Rooms
                .Remove(room);
            _dbContext.SaveChanges();
            return result;
        }

        internal List<Room> ListAll()
        {
            return _dbContext.Rooms
                .Include(r => r.Bookings)
                .ToList();
        }
        internal List<Room> ListAllAvailable(DateOnly startDate, DateOnly endDate)
        {
            return _dbContext.Rooms
                .Where(r => !r.Bookings.Any(b =>
                startDate < b.EndDate &&
                b.StartDate < endDate))
                .ToList();
        }
        internal Room? GetById(int roomId)
        {
            return _dbContext.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.Id == roomId);
        }
        internal Room? GetByRoomNumber(int roomNumber)
        {
            return _dbContext.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.RoomNumber == roomNumber);
        }
        internal Room? GetByRoomNumberAndIfAvailable(int roomNumber, DateOnly startDate, DateOnly endDate)
        {
            return _dbContext.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r =>
                r.RoomNumber == roomNumber &&
                !r.Bookings.Any(b =>
                startDate < b.EndDate &&
                b.StartDate < endDate));
        }

        internal List<Room> GetByRoomType(RoomType? roomType)
        {
            return _dbContext.Rooms
                .Include(r => r.Bookings)
                .Where(r => r.RoomType == roomType)
                .ToList();
        }
        internal List<Room> GetByRoomTypeAndAvailable(RoomType? roomType, DateOnly startDate, DateOnly endDate)
        {
            return _dbContext.Rooms
                .Include(r => r.Bookings)
                .Where(r => r.RoomType == roomType &&
                !r.Bookings.Any(b =>
                startDate < b.EndDate &&
                b.StartDate < endDate))
                .ToList();
        }
        internal Room? GetByBookingId(int bookingId)
        {
            return _dbContext.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.Bookings.Any(b => b.Id == bookingId));
        }

        internal List<Room> GetByCustomerId(int customerId)
        {
            return _dbContext.Rooms
                .Include(r => r.Bookings)
                .Where(r => r.Bookings.Any(b => b.CustomerId == customerId))
                .ToList();
        }
        private bool ValidRoomSize(int roomSizeSqM2)
        {
            return roomSizeSqM2 > 0;
        }

        private bool ValidAmountOfExtraBeds(int extraBeds, RoomType roomType, int roomSizeSqm2)
        {
            if (extraBeds < 0 || extraBeds > 2) return false;
            if (roomSizeSqm2 < 25 && extraBeds > 0) return false;
            switch (roomType)
            {
                case RoomType.Single:
                    if (extraBeds > 0)
                        return false;
                    break;

                case RoomType.Double:
                    if (roomSizeSqm2 < 30 && extraBeds > 1)
                        return false;
                    break;
            }
            return true;
        }

        private bool ValidRoomNumber(int roomNumber)
        {
            return roomNumber > 0;
        }
        private bool RoomNumberAvailable(int roomNumber, int? roomIdToExclude = null)
        {
            return !_dbContext.Rooms
                .Any(r =>
                (roomIdToExclude == null || r.Id != roomIdToExclude) &&
                r.RoomNumber == roomNumber);
        }

        private bool HasCurrentOrFutureBooking(int roomId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return _dbContext.Bookings
                .Any(b => b.RoomId == roomId &&
                b.EndDate > today);
        }

        internal List<Booking> GetAllBookingsOnRoom(int roomId)
        {
            return _dbContext.Bookings
                .Include(b => b.Room)
                .Include(b => b.Customer)
                .Where(b => b.RoomId == roomId)
                .ToList();
        }
    }
}
