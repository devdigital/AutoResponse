namespace AutoResponse.Core.ApiEvents
{
    using System;

    using AutoResponse.Core.Models;

    public class EntityValidationApiEvent : IAutoResponseApiEvent
    {
        public EntityValidationApiEvent(string code, ValidationErrorDetails errorDetails)
        {
            this.Code = code;
            this.ErrorDetails = errorDetails ?? throw new ArgumentNullException(nameof(errorDetails));
        }

        public EntityValidationApiEvent(ValidationErrorDetails errorDetails) : this("AR422", errorDetails)
        {
        }

        public string Code { get; }

        public ValidationErrorDetails ErrorDetails { get; }
    }
}