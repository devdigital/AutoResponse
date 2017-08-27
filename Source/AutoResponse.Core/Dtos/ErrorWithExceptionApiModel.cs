namespace AutoResponse.Core.Dtos
{
    public class ErrorWithExceptionApiModel : ErrorApiModel
    {
        public string ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionString { get; set; }        
    }
}