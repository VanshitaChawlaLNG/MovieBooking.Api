using MovieBooking.Contracts;
using MovieBooking.Services;
using MovieBookingContracts;
using Microsoft.EntityFrameworkCore;

namespace MovieBooking.Services.MovieServices
{
    public interface IMovieServices
    {
        public Task<APIResponse<List<MovieData>>> GetMovies();

        
        public Task<APIResponse<string>> AddMovie(MovieData movieData);

        public Task<APIResponse<string>> DeleteAMovie(string MovieName);

        public Task<APIResponse<string>> ChangeStatus(string MovieName);

        public bool CheckIfMovieExists(string movieName);

        public String ToTitleCase(string titleCase);


    }
}
