using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.Contracts;
using MovieBooking.Services.MovieServices;
using MovieBooking.Services.TimingsServices;
using System.Net;

namespace MovieBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieScheduleController : ControllerBase
    {
        private readonly IMovieTimingsServices _movieTimingServices;
        private readonly IMovieServices _movieServices;
        public MovieScheduleController(IMovieTimingsServices movieTimingsServices, IMovieServices movieServices)
        {
            _movieTimingServices = movieTimingsServices;
            _movieServices = movieServices;
        }
        [HttpPost]
        [Route("ADDTimings")]
        public async Task<APIResponse<string>> PostMovieTimingAsync(MovieTiming movieTiming)
        {
            string[] validTimes = { "Morning", "Afternoon", "Evening", "Night" };
            string inputTime = _movieServices.ToTitleCase(movieTiming.ShowTimings);

            if (!validTimes.Contains(inputTime, StringComparer.OrdinalIgnoreCase))
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Invalid Time Added" }
                };
            }
            if (movieTiming.SeatsCountLeft < 20||movieTiming.SeatsCountLeft>400)
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Movie Should Have More Than 20 Seats and less than 400" },
                    Status = HttpStatusCode.NotFound

                };
            }
            return await _movieTimingServices.AddScheduleForMovie(movieTiming);

        }

        [HttpGet]
        [Route("ShowTimings")]
        public async Task<APIResponse<List<MovieTiming>>> ShowMovieTimingAsync()
        {
            return await _movieTimingServices.GetTimings();

        }

    }
}
