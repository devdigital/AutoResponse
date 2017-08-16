namespace AutoResponse.Core.ApiEvents
{
    using System;

    using AutoResponse.Core.Models;

    public class EntityValidationApiEvent : IAutoResponseApiEvent
    {
        public EntityValidationApiEvent(string code, ValidationErrorDetails errorDetails)
        {
            if (errorDetails == null)
            {
                throw new ArgumentNullException(nameof(errorDetails));
            }

            this.Code = code;
            this.ErrorDetails = errorDetails;
        }

        public EntityValidationApiEvent(ValidationErrorDetails errorDetails) : this("AR422", errorDetails)
        {
        }

        public string Code { get; }

        public ValidationErrorDetails ErrorDetails { get; }
    }
}