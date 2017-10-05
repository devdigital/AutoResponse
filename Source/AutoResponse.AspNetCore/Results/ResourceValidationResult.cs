using AutoResponse.Core.ApiEvents;
using AutoResponse.Core.Models;

namespace AutoResponse.AspNetCore.Results
{
    public class ResourceValidationResult : AutoResponseResult
    {
        public ResourceValidationResult(ValidationErrorDetails errorDetails)
            : base(new EntityValidationApiEvent(errorDetails))
        {
        }     
    }
}