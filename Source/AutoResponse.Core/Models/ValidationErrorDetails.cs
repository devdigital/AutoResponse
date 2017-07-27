namespace AutoResponse.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationErrorDetails
    {
        public ValidationErrorDetails(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Message = message;
            this.Errors = Enumerable.Empty<ValidationError>();
        }

        public ValidationErrorDetails(string message, ValidationError error)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            this.Message = message;
            this.Errors = new List<ValidationError> { error };
        }

        public ValidationErrorDetails(string message, IEnumerable<ValidationError> errors)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            this.Message = message;
            this.Errors = errors;
        }

        public string Message { get; }

        public IEnumerable<ValidationError> Errors { get; }
    }
}