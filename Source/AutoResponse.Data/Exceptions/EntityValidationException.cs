namespace AutoResponse.Data.Exceptions
{
    using System;

    using AutoResponse.Data.Errors;

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