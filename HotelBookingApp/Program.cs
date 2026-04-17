using HotelBookingApp.Data;
using HotelBookingApp.Data.BogusSeeder;
using HotelBookingApp.Flows.Bookings;
using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Flows.Rooms;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Startup;
using HotelBookingApp.Utilities;
using Microsoft.EntityFrameworkCore;
namespace HotelBookingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using ApplicationDbContext dbContext = AppStartup.CreateDbContext();

                dbContext.Database.Migrate();
                DbSeeder.SeedIfNoData(dbContext);


                var bookingService = new BookingService(dbContext);
                var customerService = new CustomerService(dbContext);
                var roomService = new RoomService(dbContext);

                var customerFlows = new CustomerFlows(customerService);
                var roomFlows = new RoomFlows(roomService);
                var bookingFlows = new BookingFlows(bookingService, customerFlows, roomFlows);

                var app = new App(bookingFlows, customerFlows, roomFlows);
                app.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Startup failed.");
                Console.WriteLine(exception.Message);
                InputHelper.WaitForContinue();
            }
        }
    }
}
