using HotelBookingApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models
{
    internal class Room
    {
        [Key]
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        [Range(0, 2)]
        public int ExtraBeds { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
