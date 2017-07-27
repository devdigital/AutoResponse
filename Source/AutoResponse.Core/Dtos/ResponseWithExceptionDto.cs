namespace AutoResponse.Core.Dtos
{
    public class ResponseWithExceptionDto : ResponseDto
    {     
        // TODO: add exception type
        public string ExceptionMessage { get; set; }

        public string ExceptionString { get; set; }
    }
}