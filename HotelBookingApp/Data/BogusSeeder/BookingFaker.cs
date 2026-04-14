using Bogus;
using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Data.BogusSeeder
{
    internal class BookingFaker : Faker<Booking>
    {
        internal BookingFaker(IReadOnlyList<Customer> customers, IReadOnlyList<Room> rooms)
        {
            RuleFor(b => b.StartDate, f => 
                DateOnly.FromDateTime(f.Date.Between(DateTime.Today.AddDays(-30), DateTime.Today.AddMonths(3))));

            RuleFor(b => b.EndDate, (f, b) =>
                b.StartDate.AddDays(f.Random.Int(1,14)));

            RuleFor(b => b.CustomerId, f =>
                f.PickRandom(customers.ToList()).Id);

            RuleFor(b => b.RoomId, f =>
                f.PickRandom(rooms.ToList()).Id);
        }
    }
}
