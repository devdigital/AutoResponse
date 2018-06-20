// <copyright file="ResourceValidationAttribute.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.AspNetCore.Attributes
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using AutoResponse.AspNetCore.Results;
    using AutoResponse.Core.Models;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Resource validation attribute.
    /// </summary>
    public class ResourceValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The JSON settings.
        /// </summary>
        private readonly JsonSerializerSettings jsonSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceValidationAttribute"/> class.
        /// </summary>
        public ResourceValidationAttribute()
        {
            this.jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceValidationAttribute"/> class.
        /// </summary>
        /// <param name="jsonSettings">The JSON settings.</param>
        public ResourceValidationAttribute(JsonSerializerSettings jsonSettings = null)
        {
            this.jsonSettings = jsonSettings ?? throw new ArgumentNullException(nameof(jsonSettings));
        }

        /// <summary>
        /// Action filter.
        /// </summary>
        /// <param name="context">The action context.</param>
        /// <param name="next">The next delegate.</param>
        /// <returns>The task.</returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionParameters = context
                .ActionDescriptor
                .Parameters;

            // Ensure all action arguments present have valid values
            if (context.ModelState != null && !context.ModelState.IsValid)
            {
                this.SetValidationResponse(
                    context,
                    ToStandardModelStateResponse(context.ModelState));

                return;
            }

            // Ensure all action arguments marked with the Required attribute are present
            if (context.ActionArguments != null && context.ActionArguments.Any())
            {
                var actionParametersWithRequiredAttribute =
                    actionParameters.Where(p =>
                        p.ParameterType.GetCustomAttributes<System.ComponentModel.DataAnnotations.RequiredAttribute>()
                            .Any());

                foreach (var actionParameterWithRequiredAttribute in actionParametersWithRequiredAttribute)
                {
                    var actionArgument =
                        context.ActionArguments.Where(
                                a => a.Key == actionParameterWithRequiredAttribute.Name)
                            .Select(a => new { a.Key, a.Value })
                            .FirstOrDefault();

                    if (actionArgument == null)
                    {
                        throw new InvalidOperationException(
                            $"Parameter with Required attribute '{actionParameterWithRequiredAttribute.Name}' not found in action arguments");
                    }

                    if (actionArgument.Value == null)
                    {
                        // TODO: review JSON vs XML
                        this.SetValidationResponse(
                            context,
                            this.ToStandardRequestBodyMissingError(
                                this.jsonSettings,
                                actionParameterWithRequiredAttribute));

                        return;
                    }
                }
            }

            await base.OnActionExecutionAsync(context, next);
        }

        private static ValidationErrorDetails ToStandardModelStateResponse(
            ModelStateDictionary modelState)
        {
            return modelState.ToValidationErrorDetails("There was a validation error.", "resource");
        }

        private ValidationErrorDetails ToStandardRequestBodyMissingError(
            JsonSerializerSettings settings,
            ParameterDescriptor parameter)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            var typeDescription = parameter.ParameterType?.ToDescription(settings);
            var postFix = string.IsNullOrWhiteSpace(typeDescription) ? null : $" E.g. {typeDescription}.";
            var message = $"There was a validation error. A required request body is missing.{postFix}";
            return new ValidationErrorDetails(message);
        }

        private void SetValidationResponse(
            ActionExecutingContext actionContext,
            ValidationErrorDetails validationErrorDetails)
        {
            actionContext.Result = new ResourceValidationResult(
                validationErrorDetails);
        }
    }
}