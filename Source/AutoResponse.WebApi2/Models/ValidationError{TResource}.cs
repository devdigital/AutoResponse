namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Linq.Expressions;

    using AutoResponse.Data.Helpers;

    public class ValidationError<TResource, TField> : ValidationError
    {
        public ValidationError(Expression<Func<TField>> field, ValidationErrorCode code, string errorMessage = null)
            : base(typeof(TResource).Name, PropertyNameHelper.PropertyName(field), code, errorMessage)
        {
        }
    }
}