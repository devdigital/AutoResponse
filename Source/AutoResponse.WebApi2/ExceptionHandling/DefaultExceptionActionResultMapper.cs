namespace AutoResponse.WebApi2.ExceptionHandling
{
    using AutoResponse.Data.Exceptions;
    using AutoResponse.WebApi2.Extensions;
    using AutoResponse.WebApi2.Results;

    public class DefaultExceptionActionResultMapper : ExceptionActionResultMapperBase
    {
        public DefaultExceptionActionResultMapper()
        {            
            this.AddMapping<EntityValidationException>(
                (r, e) => new ResourceValidationResult(r, e.ErrorDetails.ToValidationErrorDetails()));

            // TODO: review mapping entity type to resource type?
            this.AddMapping<EntityNotFoundException>(
                (r, e) => new ResourceNotFoundResult(r, e.EntityType, e.EntityId));
            
            this.AddGenericMapping<IEntityNotFoundException>(
                typeof(EntityNotFoundException<>), 
                (r, e) => new ResourceNotFoundResult(r, e.EntityType, e.EntityId));

            this.AddMapping<EntityCreatePermissionException>(
                (r, e) => new ResourcePermissionResult(r, e.UserId, e.EntityType, e.EntityId));

            this.AddMapping<EntityPermissionException>(
                (r, e) => new ResourceCreatePermissionResult(r, e.UserId, e.EntityType, e.EntityId));
        }
    }
}