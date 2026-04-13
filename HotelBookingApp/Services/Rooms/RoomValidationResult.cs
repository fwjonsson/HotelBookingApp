using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Services.Rooms
{
    internal class RoomValidationResult
    {
        public bool RoomFound { get; set; } = true;
        public bool RoomNumberAvailable { get; set; } = true;
        public bool RoomNumberValid { get; set; } = true;
        public bool RoomTypeValid { get; set; } = true;
        public bool ExtraBedsValid { get; set; } = true;
        public bool ChoseRoomTypeBeforeExtraBeds { get; set; } = true;
        public bool BookingFound { get; set; } = true;
        public bool CustomerFound { get; set; } = true;
        public bool CanBeDeleted { get; set; } = true;

        public bool Success =>
            RoomFound &&
            RoomNumberAvailable &&
            RoomNumberValid &&
            RoomTypeValid &&
            ExtraBedsValid &&
            ChoseRoomTypeBeforeExtraBeds &&
            BookingFound &&
            CustomerFound &&
            CanBeDeleted;
    }
}
