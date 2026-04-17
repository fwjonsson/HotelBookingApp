namespace HotelBookingApp.Data.BogusSeeder
{
    internal static class DbSeeder
    {
        internal static void SeedIfNoData(ApplicationDbContext dbContext)
        {
            if (dbContext.Customers.Any() &&
                        dbContext.Rooms.Any() &&
                        dbContext.Bookings.Any())
            {
                return;
            }

            var customers = new CustomerFaker().Generate(150);
            var rooms = new RoomFaker().Generate(40);

            dbContext.Customers.AddRange(customers);
            dbContext.Rooms.AddRange(rooms);
            dbContext.SaveChanges();

            var bookings = new BookingFaker(customers, rooms).Generate(100);

            dbContext.Bookings.AddRange(bookings);
            dbContext.SaveChanges();
        }
    }
}
