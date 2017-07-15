namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.Errors;

    public class EntityValidationException : Exception
    {
        public EntityValidationException(EntityValidationErrorDetails errorDetails)
        {
            if (errorDetails == null)
            {
                throw new ArgumentNullException(nameof(errorDetails));
            }

            this.ErrorDetails = errorDetails;
        }

        public EntityValidationErrorDetails ErrorDetails { get; }
    }
}