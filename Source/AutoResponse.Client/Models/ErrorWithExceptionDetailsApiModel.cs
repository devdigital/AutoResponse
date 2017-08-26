namespace AutoResponse.Client.Models
{
    public class ErrorWithExceptionDetailsApiModel : ErrorApiModel
    {
        public string ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionString { get; set; }
    }
}