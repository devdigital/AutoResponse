namespace AutoResponse.Core.Models
{
    using System;
    using System.Linq.Expressions;

    using AutoResponse.Core.Enums;
    using AutoResponse.Core.Helpers;

    public class ValidationError<TResource> : ValidationError
    {
        public ValidationError(Expression<Func<TResource, object>> field, ValidationErrorCode code, string errorMessage = null)
            : base(typeof(TResource).Name, PropertyNameHelper.PropertyName(field), code, errorMessage)
        {
        }
    }
}