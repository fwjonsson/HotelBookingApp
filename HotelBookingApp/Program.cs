using HotelBookingApp.Data;
using HotelBookingApp.Flows.Bookings;
using HotelBookingApp.Flows.Customers;
using HotelBookingApp.Flows.Rooms;
using HotelBookingApp.Services.Bookings;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Services.Rooms;
using HotelBookingApp.Startup;
namespace HotelBookingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using ApplicationDbContext dbContext = AppStartup.CreateDbContext();

            var bookingService = new BookingService(dbContext);
            var customerService = new CustomerService(dbContext);
            var roomService = new RoomService(dbContext);

            var bookingFlows = new BookingFlows(bookingService);
            var customerFlows = new CustomerFlows(customerService);
            var roomFlows = new RoomFlows(roomService);

            var app = new App(bookingFlows, customerFlows, roomFlows);
            app.Run();
        }
    }
}
