// <copyright file="PropertyNameHelper.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Helpers
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Property name helper.
    /// </summary>
    public static class PropertyNameHelper
    {
        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns>The property name.</returns>
        public static string PropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression body)
            {
                var unaryExpression = body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member.Name;
        }
    }
}