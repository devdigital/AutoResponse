namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.Models;

    public class EntityValidationException : Exception
    {
        public EntityValidationException(ValidationErrorDetails errorDetails)
        {
            if (errorDetails == null)
            {
                throw new ArgumentNullException(nameof(errorDetails));
            }

            this.ErrorDetails = errorDetails;
        }

        public ValidationErrorDetails ErrorDetails { get; }
    }
}