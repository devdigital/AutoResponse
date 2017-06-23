namespace AutoResponse.WebApi2.ExceptionHandling
{
    using AutoResponse.Data.Exceptions;
    using AutoResponse.WebApi2.Extensions;
    using AutoResponse.WebApi2.Results;

    using Humanizer;

    public class AutoResponseExceptionActionResultMapper : ExceptionActionResultMapperBase
    {
        protected override void ConfigureMappings(ExceptionActionResultBuilder builder)
        {
            builder.AddMapping<NotAuthenticatedException>(
                (r, e) => new NotAuthenticatedResult(r));

            builder.AddMapping<EntityValidationException>(
                (r, e) => new ResourceValidationResult(r, e.ErrorDetails.ToValidationErrorDetails()));

            builder.AddMapping<EntityNotFoundException>(
                (r, e) => new ResourceNotFoundResult(r, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddGenericMapping<IEntityNotFoundException>(
                typeof(EntityNotFoundException<>),
                (r, e) => new ResourceNotFoundResult(r, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddMapping<EntityCreatePermissionException>(
                (r, e) => new ResourceCreatePermissionResult(r, e.UserId, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddGenericMapping<IEntityCreatePermissionException>(
                typeof(EntityCreatePermissionException<>),
                (r, e) => new ResourceCreatePermissionResult(r, e.UserId, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddMapping<EntityPermissionException>(
                (r, e) => new ResourcePermissionResult(r, e.UserId, e.EntityType.Kebaberize(), e.EntityId));

            builder.AddGenericMapping<IEntityPermissionException>(
                typeof(EntityPermissionException<>),
                (r, e) => new ResourcePermissionResult(r, e.UserId, e.EntityType.Kebaberize(), e.EntityId));
        }
    }
}