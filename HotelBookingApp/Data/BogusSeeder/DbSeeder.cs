using HotelBookingApp.Services.Rooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Data.BogusSeeder
{
    internal static class DbSeeder
    {
        internal static void Seed(ApplicationDbContext dbContext)
        {
            var customers = new CustomerFaker().Generate(30);
            var rooms = new RoomFaker().Generate(10);

            dbContext.Customers.AddRange(customers);
            dbContext.Rooms.AddRange(rooms);

            var savedCustomers = dbContext.Customers.AsNoTracking().ToList();
            var savedRooms = dbContext.Rooms.AsNoTracking().ToList();

            var bookings = new BookingFaker(savedCustomers, savedRooms).Generate(35);

            dbContext.Bookings.AddRange(bookings);
            dbContext.SaveChanges();
        }
    }
}
