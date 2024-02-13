
using Microsoft.EntityFrameworkCore;
using MovieBooking.DataContext.Data;
using MovieBooking.Contracts;
using System.Globalization;

namespace MovieBooking.Services.BookingServices
{
    public class ShowBookingServices : IShowBookingServices
    {
        private readonly DataContexts _context;

        public ShowBookingServices(DataContexts context)
        {
            _context = context;
        }
        public async Task<APIResponse<string>> MovieBooking(string MovieName, string UsersPreferedTiming, int SeatsNeeded)
        {
            try
            {
       
                    string inputTime = ToTitleCase(UsersPreferedTiming);
                var existingMovie = await _context.MovieData.FirstOrDefaultAsync(m => m.MovieName ==ToTitleCase(MovieName));
                if (existingMovie != null)
                {
                    var movieName = await _context.MovieData.FirstOrDefaultAsync(m => m.MovieName == ToTitleCase(MovieName));
                    if ((bool)!movieName.Status)
                    {
                        return new APIResponse<string>
                        {
                            Error = new Error { errorMessage = "Movie Currently InActive" },
                            Status = System.Net.HttpStatusCode.BadRequest
                        };
                    }
                    var showTiming = await _context.MovieTiming.FirstOrDefaultAsync(t => t.ShowTimings == inputTime);

                    if (showTiming == null)
                    {
                        return new APIResponse<string>
                        {
                            Error = new Error { errorMessage = "No Slot For You According To Your Preference" },
                            Status = System.Net.HttpStatusCode.BadRequest
                        };
                    }

                    int seatsLeftAfterBooking = (int)(showTiming.SeatsCountLeft - SeatsNeeded);

                    if (seatsLeftAfterBooking < 0)
                    {
                        return new APIResponse<string>
                        {
                            Error = new Error { errorMessage = "Not enough seats available" },
                            Status = System.Net.HttpStatusCode.BadRequest
                        };
                    }


                    var booking = new MovieBookings
                    {
                        MovieName = ToTitleCase(MovieName),
                        PreferedSlot = ToTitleCase(UsersPreferedTiming),
                        SeatsBooked = SeatsNeeded
                    };

                    _context.MovieBookings.Add(booking);
                    _context.SaveChanges();

                    showTiming.SeatsCountLeft = seatsLeftAfterBooking;
                    _context.SaveChanges();

                    return new APIResponse<string>
                    {
                        data = $"Movie '{MovieName}' Booking successful",
                        Status = System.Net.HttpStatusCode.OK
                    };
                }

                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = "Movie Not available" },
                    Status = System.Net.HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<string>
                {
                    Error = new Error { errorMessage = ex.Message },
                    Status = System.Net.HttpStatusCode.InternalServerError
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