using Microsoft.EntityFrameworkCore;
using MovieBooking.Contracts;
using MovieBookingContracts;

namespace MovieBooking.DataContext.Data
{

    public class DataContexts : DbContext
    {

        public DataContexts(DbContextOptions<DataContexts> options) : base(options)
        {

        }
        public DbSet<MovieData> MovieData { get; set; }
        public DbSet<MovieTiming> MovieTiming { get; set; }
        public DbSet<MovieBookings> MovieBookings { get; set; }

      
    }
}
