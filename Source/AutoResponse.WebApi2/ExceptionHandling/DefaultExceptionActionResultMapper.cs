namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;

    using AutoResponse.Data.Errors;
    using AutoResponse.Data.Exceptions;
    using AutoResponse.WebApi2.Results;

    public class DefaultExceptionActionResultMapper : ExceptionActionResultMapperBase
    {
        public DefaultExceptionActionResultMapper()
        {
            this.AddMapping<EntityValidationException>((r, e) => new ResourceValidationResult(
                r, e.ErrorDetails.ToValidationErrorDetails()));

            // TODO: review mapping entity type to resource type?
            this.AddMapping<EntityNotFoundException>((r, e) => new ResourceNotFoundActionResult(r, e.EntityType, e.EntityId));
            
            this.AddMapping<EntityPermissionException>(
                (r, e) => new ResourcePermissionActionResult(r, e.UserId, e.EntityType, e.EntityId));
        }
    }

    internal static class EntityValidationErrorCodeExtensions
    {
        public static ValidationErrorCode ToValidationErrorCode(this EntityValidationErrorCode errorCode)
        {
            switch (errorCode)
            {
                case EntityValidationErrorCode.None:
                    return ValidationErrorCode.None;
                case EntityValidationErrorCode.Missing:
                    return ValidationErrorCode.Missing;
                case EntityValidationErrorCode.MissingField:
                    return ValidationErrorCode.MissingField;
                case EntityValidationErrorCode.Invalid:
                    return ValidationErrorCode.Invalid;
                case EntityValidationErrorCode.AlreadyExists:
                    return ValidationErrorCode.AlreadyExists;
                default:
                    throw new ArgumentOutOfRangeException(nameof(errorCode), errorCode, null);
            }
        }
    }
}