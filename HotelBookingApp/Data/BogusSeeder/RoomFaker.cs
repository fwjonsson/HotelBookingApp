using Bogus;
using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Data.BogusSeeder
{
    internal sealed class RoomFaker : Faker<Room>
    {
        internal RoomFaker()
        {
            RuleFor(r => r.RoomNumber, f => 100 + f.UniqueIndex);

            RuleFor(r => r.RoomType, f =>
                f.PickRandom<RoomType>());

            RuleFor(r => r.ExtraBeds, (f, r) =>
                r.RoomType == RoomType.Single
                    ? 0
                    : f.Random.Int(0, 2));
        }
    }
}
