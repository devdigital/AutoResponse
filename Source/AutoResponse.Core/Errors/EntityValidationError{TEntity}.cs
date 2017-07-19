namespace AutoResponse.Core.Errors
{
    using System;
    using System.Linq.Expressions;

    using AutoResponse.Core.Helpers;

    public class EntityValidationError<TEntity, TProperty> : EntityValidationError
    {
        public EntityValidationError(Expression<Func<TEntity, TProperty>> property, EntityValidationErrorCode code, string message = null)
            : base(typeof(TEntity).Name, PropertyNameHelper.PropertyName(property), code, message)
        {
        }
    }
}