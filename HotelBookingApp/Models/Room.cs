using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using HotelBookingApp.Enums;

namespace HotelBookingApp.Models
{
    internal class Room
    {
        [Key]
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        [Range (0,2)]
        public int ExtraBeds { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
