using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models
{
    internal class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
    }
}
