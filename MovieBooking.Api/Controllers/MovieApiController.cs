using Microsoft.AspNetCore.Mvc;
using MovieBooking.Contracts;
using MovieBooking.Services.BookingServices;
using MovieBooking.Services.MovieServices;
using MovieBooking.Services.TimingsServices;
using MovieBookingContracts;
using System.Net.NetworkInformation;
using System.Net;

namespace MovieBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieApiController : ControllerBase
    {

        private readonly IMovieServices _movieServices;



        public MovieApiController(IMovieServices movieServices)
        {
            _movieServices = movieServices;

        }


        [HttpGet]
        [Route("GetAllMovies")]
        public async Task<APIResponse<List<MovieData>>> GetMoviesAsync()
        {

            return await _movieServices.GetMovies();

        }

        [HttpPost]
        [Route("AddMovie")]
        public async Task<APIResponse<string>> InsertMovieAsync(MovieData movieAdd)

        {
            if (string.IsNullOrWhiteSpace(movieAdd.MovieName) || movieAdd.MovieName.ToLower() == "string")
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Enter a valid Movie Name" },
                    Status = HttpStatusCode.BadRequest
                };
            }

            if (string.IsNullOrWhiteSpace(movieAdd.TheatreName) || movieAdd.TheatreName.ToLower() == "string")
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Theatre Name can't be an empty string" },
                    Status = HttpStatusCode.BadRequest
                };
            }
            if (movieAdd.TheatreName.ToUpper() !="PVR")
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Their is Only One Theatre Available i.e PVR" },
                    Status = HttpStatusCode.BadRequest
                };
            }
            if (string.IsNullOrWhiteSpace(movieAdd.Genre) || movieAdd.Genre.ToLower() == "string")
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Genre can't be an empty string" },
                    Status = HttpStatusCode.BadRequest
                };
            }

            if (string.IsNullOrWhiteSpace(movieAdd.MovieDirector) || movieAdd.MovieDirector.ToLower() == "string")
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Enter a valid Director Name" },
                    Status = HttpStatusCode.BadRequest
                };
            }

            if (movieAdd.Duration <= 0 || movieAdd.Duration >= 240)
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Duration must be greater than zero and less than 240 minutes" },
                    Status = HttpStatusCode.BadRequest
                };
            }
            if (_movieServices.CheckIfMovieExists(movieAdd.MovieName))
            {
                return new APIResponse<string>
                {
                    Error = new Error
                    {
                        errorMessage = "Movie Already Exists"

                    },
                    Status = HttpStatusCode.BadRequest
                };
            }

            return await _movieServices.AddMovie(movieAdd);
        }


        [HttpDelete]
        [Route("DeleteAMovie")]
        public async Task<APIResponse<string>> DeleteMovieAsync(String movieName)
        {
            if (string.IsNullOrWhiteSpace(movieName) || movieName.ToLower() == "string")
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Enter a valid Movie Name" },
                    Status = HttpStatusCode.BadRequest
                };
            }
            return await _movieServices.DeleteAMovie(movieName);

        }

        [HttpPut]
        [Route("UpdateStatus")]
        public async Task<APIResponse<string>> PutMovie(String MovieName)
        {
            if (string.IsNullOrWhiteSpace(MovieName) || MovieName.ToLower() == "string")
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Enter a valid Movie Name" },
                    Status = HttpStatusCode.BadRequest
                };
            }
            return await _movieServices.ChangeStatus(MovieName);

        }




    }
}
