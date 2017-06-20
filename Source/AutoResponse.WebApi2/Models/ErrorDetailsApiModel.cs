namespace AutoResponse.WebApi2.Results
{
    using System.Collections.Generic;

    public class ErrorDetailsApiModel
    {
        public string Message { get; set; }

        public IEnumerable<ErrorApiModel> Errors { get; set; }
    }
}