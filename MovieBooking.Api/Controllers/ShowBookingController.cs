using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBooking.Contracts;
using MovieBooking.DataContext.Data;
using MovieBooking.Services.BookingServices;
using MovieBooking.Services.MovieServices;

namespace MovieBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowBookingController : ControllerBase
    {
        private readonly IShowBookingServices _movieBookingServices;
        private readonly IMovieServices _movieServices;

        public ShowBookingController(IShowBookingServices showBookingServices, IMovieServices movieServices)
        {
            _movieBookingServices = showBookingServices;
            _movieServices = movieServices;


        }
        [HttpPost]
        [Route("BookingMovie")]
        public async Task<APIResponse<string>> MovieBookingsAsync(String MovieName, String UsersPreferedTiming, int SeatsNeeded)
        {
            string[] validTimes = { "Morning", "Afternoon", "Evening", "Night" };
            string inputTime = _movieServices.ToTitleCase(UsersPreferedTiming);

            if (!validTimes.Contains(inputTime, StringComparer.OrdinalIgnoreCase))
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Invalid Time Added" },
                    Status = System.Net.HttpStatusCode.BadRequest
                };

            }

            return await _movieBookingServices.MovieBooking(MovieName, UsersPreferedTiming, SeatsNeeded);

        }

    }
}
