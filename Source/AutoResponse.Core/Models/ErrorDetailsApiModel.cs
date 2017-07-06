namespace AutoResponse.Core.Models
{
    using System.Collections.Generic;

    public class ErrorDetailsApiModel<TError>
    {
        public string Message { get; set; }

        public IEnumerable<TError> Errors { get; set; }
    }
}