namespace AutoResponse.Core.ApiEvents
{
    using System;

    using AutoResponse.Core.Models;

    public class EntityValidationApiEvent
    {        
        public EntityValidationApiEvent(ValidationErrorDetails errorDetails)           
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