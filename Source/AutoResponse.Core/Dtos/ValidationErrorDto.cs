namespace AutoResponse.Core.Dtos
{
    public class ValidationErrorDto
    {
        public string Resource { get; set; }

        public string Field { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}