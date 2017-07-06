namespace AutoResponse.Core.Models
{
    using System;
    using System.Linq.Expressions;

    using AutoResponse.Core;
    using AutoResponse.Core.Helpers;

    public class ValidationError<TResource, TField> : ValidationError
    {
        public ValidationError(Expression<Func<TField>> field, ValidationErrorCode code, string errorMessage = null)
            : base(typeof(TResource).Name, PropertyNameHelper.PropertyName(field), code, errorMessage)
        {
        }
    }
}