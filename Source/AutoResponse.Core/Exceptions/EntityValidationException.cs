namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Models;

    public class EntityValidationException : AutoResponseException
    {
        public EntityValidationException(string code, ValidationErrorDetails errorDetails)
            : base(ToMessage(errorDetails), new EntityValidationApiEvent(code, errorDetails))
        {            
        }

        public EntityValidationException(ValidationErrorDetails errorDetails) 
            : base(ToMessage(errorDetails), new EntityValidationApiEvent(errorDetails))
        {
        }

        private static string ToMessage(ValidationErrorDetails errorDetails)
        {
            return errorDetails?.Message;
        }
    }
}