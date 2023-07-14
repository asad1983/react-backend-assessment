namespace React_Backend.Application.Errors
{
    public class ApiValidationErrorResponce : ApiResponce
    {
        public ApiValidationErrorResponce() : base(400)
        {

        }
        public IEnumerable<string> Errors { get; set; }

    }
}
