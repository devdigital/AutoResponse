namespace AutoResponse.Core.Dtos
{
    using System.Collections.Generic;

    public class ValidationErrorDetailsApiModel : ErrorApiModel
    {        
        public IEnumerable<ValidationErrorDto> Errors { get; set; }
    }
}