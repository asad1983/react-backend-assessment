namespace React_Backend.Application.Errors
{
    public class ApiResponce
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponce(int statusCode, string message = null)
        {
            this.StatusCode = statusCode;
            Message = message ?? DefaultStatusCodeMessage(statusCode);
        }
        private string DefaultStatusCodeMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request you have made",
                401 => "Authorized you have not",
                404 => "Resource Found it was not",
                500 => "Errors are the path to the dark side.  Errors lead to anger.   Anger leads to hate.  Hate leads to career change.",
                0 => "Some Thing Went Wrong",
                _ => throw new NotImplementedException()
            };
        }
    }
}
