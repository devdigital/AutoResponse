namespace AutoResponse.Core.Dtos
{
    using System.Collections.Generic;

    public class ValidationErrorDetailsDto : ErrorDto
    {        
        public IEnumerable<ValidationErrorDto> Errors { get; set; }
    }
}