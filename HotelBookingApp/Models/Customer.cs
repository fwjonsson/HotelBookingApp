using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models
{
    internal class Customer
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        public DateOnly Birthdate { get; set; }
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
