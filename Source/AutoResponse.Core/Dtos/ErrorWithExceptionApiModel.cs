namespace AutoResponse.Core.Dtos
{
    public class ErrorWithExceptionApiModel : ErrorApiModel
    {     
        // TODO: add exception type
        public string ExceptionMessage { get; set; }

        public string ExceptionString { get; set; }
    }
}