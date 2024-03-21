using MovieBooking.Contracts;

namespace MovieBooking.Services.TimingsServices
{
    //ITiming Services
    public interface IMovieTimingsServices
    {
        public Task<APIResponse<string>> AddScheduleForMovie(MovieTiming movieTiming);
        public Task<APIResponse<List<MovieTiming>>> GetTimings();
    }
}
