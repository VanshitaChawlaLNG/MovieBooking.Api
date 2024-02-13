using MovieBooking.Contracts;

namespace MovieBookingContracts
{
    public class MovieData
    {
        public int ID { get; set; }

        public string? MovieName { get; set; }

        public string? Genre { get; set; }

        public string? MovieDirector { get; set; }

        public string? TheatreName { get; set; }

        public int? Duration { get; set; }

        public bool? Status { get; set; }

    }
}
