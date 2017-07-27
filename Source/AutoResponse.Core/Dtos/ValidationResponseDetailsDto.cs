namespace AutoResponse.Core.Dtos
{
    using System.Collections.Generic;

    public class ValidationResponseDetailsDto : ResponseDto
    {        
        public IEnumerable<ValidationErrorDto> Errors { get; set; }
    }
}