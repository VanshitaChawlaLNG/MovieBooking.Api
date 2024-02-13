using System.Net;


namespace MovieBooking.Contracts
{

    public class APIResponse<T> where T : class
    {
        public T data { get; set; }
        public Error Error { get; set; }
        public HttpStatusCode Status { get; set; }
    }

    public class Error
    {
        public string errorMessage { get; set; }
    }
}

