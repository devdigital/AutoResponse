using AutoFixture.Kernel;

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using System;
    using System.Reflection;

    internal class AutoInjectBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var parameterInfo = request as ParameterInfo;
            if (parameterInfo != null)
            {
                return this.HandleParameterInfo(parameterInfo);
            }

            var propertyInfo = request as PropertyInfo;
            if (propertyInfo != null)
            {
                return this.HandlePropertyInfo(propertyInfo, context);
            }

            return new NoSpecimen();
        }        

        private object HandleParameterInfo(ParameterInfo pi)
        {
            if (pi.ParameterType.IsEnum)
            {
                var enumValues = Enum.GetValues(pi.ParameterType);
                return enumValues.Length > 1 ? enumValues.GetValue(1) : new NoSpecimen();
            }

            return new NoSpecimen();
        }

        private object HandlePropertyInfo(PropertyInfo propertyInfo, ISpecimenContext context)
        {
            return new NoSpecimen();
        }
    }
}