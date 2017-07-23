namespace AutoResponse.Core.Dtos
{
    public class ErrorWithExceptionDto : ErrorDto
    {     
        // TODO: add exception type
        public string ExceptionMessage { get; set; }

        public string ExceptionString { get; set; }
    }
}