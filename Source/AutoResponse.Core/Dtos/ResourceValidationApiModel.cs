namespace AutoResponse.Core.Dtos
{
    using System.Collections.Generic;

    public class ResourceValidationApiModel : ErrorApiModel
    {        
        public IEnumerable<ResourceValidationErrorApiModel> Errors { get; set; }
    }
}