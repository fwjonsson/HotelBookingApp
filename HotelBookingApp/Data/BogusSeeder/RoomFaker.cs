using Bogus;
using HotelBookingApp.Enums;
using HotelBookingApp.Models;

namespace HotelBookingApp.Data.BogusSeeder
{
    internal sealed class RoomFaker : Faker<Room>
    {
        internal RoomFaker()
        {
            RuleFor(r => r.RoomNumber, f => 200 + f.UniqueIndex);

            RuleFor(r => r.RoomType, f =>
                f.PickRandom<RoomType>());

            RuleFor(r => r.RoomSizeSqm2, f =>
                f.Random.Int(20, 45));

            RuleFor(r => r.ExtraBeds, (f, r) =>
                r.RoomType == RoomType.Single || r.RoomSizeSqm2 < 25
                    ? 0
                    : r.RoomSizeSqm2 < 30
                    ? f.Random.Int(0, 1)
                    : f.Random.Int(0, 2));
        }
    }
}
