namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    public class EntityValidationException : AutoResponseException<EntityValidationApiEvent>
    {
        public EntityValidationException(string code, ValidationErrorDetails errorDetails)
            : base(new EntityValidationApiEvent(code, errorDetails))
        {            
        }

        public EntityValidationException(ValidationErrorDetails errorDetails) 
            : base(new EntityValidationApiEvent(errorDetails))
        {
        }
    }
}