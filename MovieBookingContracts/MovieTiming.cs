using MovieBookingContracts;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieBooking.Contracts
{
    public class MovieTiming
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string? MovieName { get; set; }
        public string? ShowTimings { get; set; }
        public int? SeatsCountLeft { get; set; }

    }
}
