namespace AutoResponse.Client.Models
{
    using System.Collections.Generic;

    public class ErrorDetailsApiModel : ErrorApiModel
    {        
        public IEnumerable<ValidationErrorApiModel> Errors { get; set; }
    }
}