
using MovieBooking.Contracts;

namespace MovieBooking.Services.BookingServices
{
    public interface IShowBookingServices
    {
        public Task<APIResponse<string>> MovieBooking(String Movie, String UsersPreferedTiming,int SeatsNeeded);
    }
}
