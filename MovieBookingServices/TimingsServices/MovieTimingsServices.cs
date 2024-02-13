
using MovieBooking.Contracts;
using Microsoft.EntityFrameworkCore;
using MovieBooking.DataContext.Data;
using System.Net;
using System.Globalization;

namespace MovieBooking.Services.TimingsServices
{
    public class MovieTimingsServices : IMovieTimingsServices
    {
        private readonly DataContexts _context;

        public MovieTimingsServices(DataContexts context)
        {
            _context = context;
        }

        // Add Schedule For a Movie
        public async Task<APIResponse<string>> AddScheduleForMovie(MovieTiming movieTiming)
        {
            try
            {
                var movieNameTitleCase = ToTitleCase(movieTiming.MovieName);
                var showTimingsTitleCase = ToTitleCase(movieTiming.ShowTimings);

                var existingMovie = await _context.MovieData.FirstOrDefaultAsync(m => m.MovieName == movieNameTitleCase);

                if (existingMovie != null)
                {
                    var existingTiming = await _context.MovieTiming.FirstOrDefaultAsync(t =>
                        t.ShowTimings == showTimingsTitleCase && t.MovieName == movieNameTitleCase);

                    if (existingTiming != null)
                    {
                        return new APIResponse<string>
                        {
                            Error = new Error { errorMessage = "Timing Already Exists" },
                            Status = HttpStatusCode.BadRequest,
                        };
                    }
                    var movieName = await _context.MovieData.FirstOrDefaultAsync(m => m.MovieName == movieNameTitleCase);
                    if ((bool)!movieName.Status)
                    {
                        return new APIResponse<string>
                        {
                            Error = new Error { errorMessage = "Movie Currently InActive" },
                            Status = System.Net.HttpStatusCode.BadRequest
                        };
                    }
                    var newMovieTiming = new MovieTiming
                    {
                        MovieName = movieNameTitleCase,
                        ShowTimings = showTimingsTitleCase,
                        SeatsCountLeft = movieTiming.SeatsCountLeft
                    };

                    await _context.MovieTiming.AddAsync(newMovieTiming);
                    await _context.SaveChangesAsync();

                    return new APIResponse<string>
                    {
                        data = $"Movie '{movieNameTitleCase}' Timing inserted with Id: {newMovieTiming.Id}",
                        Status = HttpStatusCode.OK
                    };
                }

                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = $"Movie Doesn't Exist with Name '{movieNameTitleCase}'" },
                    Status = HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = ex.ToString() },
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

        // Show Timings Added For a Movie
        public async Task<APIResponse<List<MovieTiming>>> GetTimings()
        {
            try
            {

                var timings = await _context.MovieTiming.ToListAsync();

                if (timings.Count > 0)
                {
                    return new APIResponse<List<MovieTiming>>
                    {
                        data = timings,
                        Status = HttpStatusCode.OK,
                    };
                }

                return new APIResponse<List<MovieTiming>>
                {
                    Error = new Error { errorMessage = "Movies Timings Donot Exist yet" },
                    Status = HttpStatusCode.BadRequest,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<List<MovieTiming>>
                {
                    Error = new Error { errorMessage = ex.Message },
                    Status = HttpStatusCode.InternalServerError
                };

            }
        }

        // Convert String To Title Case
        public string ToTitleCase(string input)
        {
            input = input.Trim();
            string[] words = input.Split(' ');


            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i].ToLower());
            }

            // Join the words back into a single string
            string result = string.Join(" ", words);

            return result;
        }
    }
}
