namespace AutoResponse.Core.Dtos
{
    public class ResponseWithExceptionDto : ResponseDto
    {
        public string ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionString { get; set; }        
    }
}