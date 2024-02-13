using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBooking.Data.Data
{
    public class DataContext:DbContext

    {

        public class DataContext : DbContext
        {

            public DataContext(DbContextOptions<DataContext> options) : base(options)
            {

            }
            public DbSet<MovieData> MovieData { get; set; }
            public DbSet<MovieTiming> MovieTiming { get; set; }
            public DbSet<MovieBooking> MovieBooking { get; set; }

            /*protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<MovieData>()
                        .HasKey(m => new { m.MovieName });


                modelBuilder.Entity<MovieTiming>()
                 .HasKey(t => t.TimingId);


                modelBuilder.Entity<MovieTiming>()
               .HasOne(t => t.MovieName)
               .WithMany(m => m.MovieTiming)
               .HasForeignKey(t => t.MovieName);
            }*/
        }
    }
}
