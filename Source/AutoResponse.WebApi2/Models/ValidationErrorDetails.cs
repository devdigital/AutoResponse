namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Collections.Generic;

    public class ValidationErrorDetails
    {
        public ValidationErrorDetails(string message, IEnumerable<ValidationError> errors = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Message = message;
            this.Errors = errors;
        }

        public string Message { get; }

        public IEnumerable<ValidationError> Errors { get; set; }
    }
}