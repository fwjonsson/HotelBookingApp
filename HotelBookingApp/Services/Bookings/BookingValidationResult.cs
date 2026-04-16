namespace HotelBookingApp.Services.Bookings
{
    internal class BookingValidationResult
    {
        public bool BookingFound { get; set; } = true;
        public bool EndDateIsAfterStartDate { get; set; } = true;
        public bool ValidDateRange { get; set; } = true;
        public bool CustomerFound { get; set; } = true;
        public bool RoomFound { get; set; } = true;
        public bool RoomAvailable { get; set; } = true;

        public bool Success =>
            BookingFound &&
            EndDateIsAfterStartDate &&
            ValidDateRange &&
            CustomerFound &&
            RoomFound &&
            RoomAvailable;
    }
}
