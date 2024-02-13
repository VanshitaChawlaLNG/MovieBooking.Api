using MovieBooking.Contracts;

namespace MovieBooking.Services.TimingsServices
{
    public interface IMovieTimingsServices
    {
        public Task<APIResponse<string>> AddScheduleForMovie(MovieTiming movieTiming);
        public Task<APIResponse<List<MovieTiming>>> GetTimings();
    }
}
