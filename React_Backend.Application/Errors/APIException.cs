namespace React_Backend.Application.Errors
{
    public class ApiException : ApiResponce
    {
        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            details = Details;
        }
        public string Details { get; set; }

    }
}
