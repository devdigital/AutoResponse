namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    public class EntityValidationException : AutoResponseException
    {
        public EntityValidationException(ValidationErrorDetails errorDetails) 
            : base(new EntityValidationApiEvent(errorDetails))
        {
        }
    }
}