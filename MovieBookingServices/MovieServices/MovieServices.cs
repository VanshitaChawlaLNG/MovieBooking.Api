
using MovieBooking.Contracts;
using MovieBooking.DataContext.Data;
using MovieBookingContracts;
using System.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Net;



namespace MovieBooking.Services.MovieServices
{
    public class MovieServices : IMovieServices
    {
        private readonly DataContexts _context;


        public MovieServices(DataContexts context)
        {
            _context = context;
        }

        // Method to get all movies
        public async Task<APIResponse<List<MovieData>>> GetMovies()
        {
            try
            {
                var movies = await _context.MovieData.Where(m => m.Status == true).ToListAsync();

                if (movies == null || movies.Count == 0)
                {
                    return new APIResponse<List<MovieData>>
                    {

                        Error = new Error
                        {
                            errorMessage = "No movies found."
                        },
                        Status = HttpStatusCode.NotFound
                    };
                }



                return new APIResponse<List<MovieData>>
                {
                    data = movies,

                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<List<MovieData>>
                {
                    Error = new Error { errorMessage = ex.Message },
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

        // Method to add a new movie
        public async Task<APIResponse<string>> AddMovie(MovieData MovieAdd)
        {
            try
            {
                MovieAdd.MovieName = ToTitleCase(MovieAdd.MovieName);
                MovieAdd.MovieDirector = ToTitleCase(MovieAdd.MovieDirector);
                MovieAdd.TheatreName = ToTitleCase(MovieAdd.TheatreName);
                MovieAdd.Genre = ToTitleCase(MovieAdd.Genre);
                await _context.MovieData.AddAsync(MovieAdd);
                await _context.SaveChangesAsync();

                return new APIResponse<string>
                {
                    data = $"Movie {MovieAdd.MovieName} Added Successfully",
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = ex.Message },
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

        // Method to check if a movie exists
        public bool CheckIfMovieExists(string movieName)
        {

            return _context.MovieData.Any(m => m.MovieName == ToTitleCase(movieName));
        }

        // Method to delete a movie
        public async Task<APIResponse<string>> DeleteAMovie(string MovieNameToDelete)
        {

            try
            {
                var movieToDelete = await _context.MovieData.FirstOrDefaultAsync(m => m.MovieName == ToTitleCase(MovieNameToDelete));

                if (movieToDelete != null)
                {


                    _context.MovieData.RemoveRange(movieToDelete);


                    await _context.SaveChangesAsync();

                    // Delete associated records in other tables
                    var timingsToDelete = _context.MovieTiming.Where(t => t.MovieName == MovieNameToDelete);
                    var bookingsToDelete = _context.MovieBookings.Where(b => b.MovieName == MovieNameToDelete);

                    _context.MovieTiming.RemoveRange(timingsToDelete);
                    _context.MovieBookings.RemoveRange(bookingsToDelete);

                   
                    await _context.SaveChangesAsync();

                    return new APIResponse<string>
                    {
                        data = $"Movie with name '{MovieNameToDelete}' deleted successfully",
                        Status = HttpStatusCode.OK
                    };
                }

                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = $"No movie found with name '{MovieNameToDelete}'. Delete operation failed." },
                    Status = HttpStatusCode.NotFound
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Error: " + ex.Message },
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

        // Method to change the status of a movie
        public async Task<APIResponse<string>> ChangeStatus(string MovieName)
        {
            MovieName = ToTitleCase(MovieName);
            try
            {
                var movieToUpdate = await _context.MovieData.FirstOrDefaultAsync(m => m.MovieName == MovieName);

                if (movieToUpdate != null)
                {
                    movieToUpdate.Status = !movieToUpdate.Status;

                    await _context.SaveChangesAsync();

                    return new APIResponse<string>
                    {
                        data = $"Status of {MovieName} Changed to {movieToUpdate.Status}",
                        Status = HttpStatusCode.OK
                    };
                }
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = $"Movie with name {MovieName} DoNot Exists" },
                    Status = HttpStatusCode.NotFound
                };


            }
            catch (Exception ex)
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = ex.Message },
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }





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

