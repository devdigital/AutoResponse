// <copyright file="AutoInjectBuilder.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using System;
    using System.Reflection;
    using AutoFixture.Kernel;

    /// <summary>
    /// Auto inject builder.
    /// </summary>
    /// <seealso cref="AutoFixture.Kernel.ISpecimenBuilder" />
    internal class AutoInjectBuilder : ISpecimenBuilder
    {
        /// <inheritdoc />
        public object Create(object request, ISpecimenContext context)
        {
            if (request is ParameterInfo parameterInfo)
            {
                return HandleParameterInfo(parameterInfo);
            }

            var propertyInfo = request as PropertyInfo;
            return propertyInfo != null ? HandlePropertyInfo(propertyInfo, context) : new NoSpecimen();
        }

        private static object HandleParameterInfo(ParameterInfo pi)
        {
            if (pi.ParameterType.IsEnum)
            {
                var enumValues = Enum.GetValues(pi.ParameterType);
                return enumValues.Length > 1 ? enumValues.GetValue(1) : new NoSpecimen();
            }

            return new NoSpecimen();
        }

        private static object HandlePropertyInfo(PropertyInfo propertyInfo, ISpecimenContext context)
        {
            return new NoSpecimen();
        }
    }
}