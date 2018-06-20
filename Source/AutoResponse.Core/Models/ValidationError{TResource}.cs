// <copyright file="ValidationError{TResource}.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Models
{
    using System;
    using System.Linq.Expressions;

    using AutoResponse.Core.Enums;
    using AutoResponse.Core.Helpers;

    /// <summary>
    /// Validation error.
    /// </summary>
    /// <typeparam name="TResource">The type of the resource.</typeparam>
    /// <seealso cref="AutoResponse.Core.Models.ValidationError" />
    public class ValidationError<TResource> : ValidationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError{TResource}"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="code">The code.</param>
        /// <param name="errorMessage">The error message.</param>
        public ValidationError(Expression<Func<TResource, object>> field, ValidationErrorCode code, string errorMessage = null)
            : base(typeof(TResource).Name, PropertyNameHelper.PropertyName(field), code, errorMessage)
        {
        }
    }
}