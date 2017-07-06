namespace AutoResponse.Core.Dtos
{
    using System.Collections.Generic;

    public class ValidationDetailsErrorDto
    {
        public string Message { get; set; }

        public IEnumerable<ValidationErrorDto> Errors { get; set; }
    }
}