
using System.ComponentModel.DataAnnotations;


namespace MovieBooking.Contracts
{
    public class MovieBookings
    {
        [Key]
        public int Id { get; set; }
        public string? MovieName { get; set; }
        public string? PreferedSlot { get; set; }
        public int? SeatsBooked { get; set; }

    }
}
