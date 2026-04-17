using HotelBookingApp.Services.Rooms;

namespace HotelBookingApp.Utilities.Printers.Rooms
{
    internal static class RoomValidationResultPrinter
    {
        internal static void PrintErrorsOrSuccess(RoomValidationResult result, string messageOnSuccess)
        {
            if (result.Success)
            {
                Console.WriteLine(messageOnSuccess);
                return;
            }
            if (!result.RoomFound)
            {
                Console.WriteLine("Room not found.");
                return;
            }
            if (!result.RoomNumberAvailable)
                Console.WriteLine("Room number is not available.");

            if (!result.RoomNumberValid)
                Console.WriteLine("Not valid room number.");

            if (!result.RoomTypeValid)
                Console.WriteLine("Not valid room type. Must be 1 or 2.");

            if(!result.RoomSizeSqM2Valid)
                Console.WriteLine("Not valid room size. Must be > 0.");

            if (!result.ExtraBedsValid)
                Console.WriteLine("Not valid amount of extra beds. Max 1 for single room and max 2 for double.");

            if (!result.ChoseRoomTypeBeforeExtraBeds)
                Console.WriteLine("You must choose room type before you can choose extra beds.");

            if (!result.BookingFound)
                Console.WriteLine("Booking not found.");

            if (!result.CustomerFound)
                Console.WriteLine("Customer not found.");

            if (!result.CanBeDeleted)
                Console.WriteLine("Room cannot be deleted. You cannot delete rooms with current or future bookings.");
        }
    }
}
