namespace AutoResponse.WebApi2.ExceptionHandling
{
    using AutoResponse.Data.Exceptions;
    using AutoResponse.WebApi2.Extensions;
    using AutoResponse.WebApi2.Results;

    public class DefaultExceptionActionResultMapper : ExceptionActionResultMapperBase
    {
        public DefaultExceptionActionResultMapper()
        {            
            this.AddMapping<EntityValidationException>((r, e) => new ResourceValidationResult(         
                r, e.ErrorDetails.ToValidationErrorDetails()));

            // TODO: review mapping entity type to resource type?
            this.AddMapping<EntityNotFoundException>((r, e) => new ResourceNotFoundActionResult(r, e.EntityType, e.EntityId));
            
            // this.AddGenericMapping<(typeof(EntityNotFoundException<>),);

            this.AddMapping<EntityCreatePermissionException>(
                (r, e) => new ResourcePermissionActionResult(r, e.UserId, e.EntityType, e.EntityId));

            this.AddMapping<EntityPermissionException>(
                (r, e) => new ResourceCreatePermissionResult(r, e.UserId, e.EntityType, e.EntityId));

            
        }
    }
}