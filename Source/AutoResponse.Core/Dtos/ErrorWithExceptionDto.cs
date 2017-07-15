namespace AutoResponse.Core.Dtos
{
    public class ErrorWithExceptionDto
    {
        public string Message { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionString { get; set; }
    }
}