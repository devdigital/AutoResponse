namespace AutoResponse.WebApi2.Autofac.Filters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using System.Web.Http.ModelBinding;

    using AutoResponse.WebApi2.Extensions;
    using AutoResponse.WebApi2.Results;

    using global::Autofac.Integration.WebApi;

    using Newtonsoft.Json;

    public class ResourceValidationFilter : IAutofacActionFilter
    {
        public Task OnActionExecutedAsync(
            HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext == null)
            {
                return;
            }

            var actionParameters = actionContext.ActionDescriptor.GetParameters();

            // Ensure all action arguments present have valid values
            if (actionContext.ModelState != null && !actionContext.ModelState.IsValid)
            {                
                await
                    this.SetValidationResponse(
                        actionContext,
                        cancellationToken,
                        this.ToStandardModelStateResponse(actionContext.ModelState, actionParameters));

                return;
            }

            // Ensure all action arguments marked with the Required attribute are present
            if (actionContext.ActionArguments != null && actionContext.ActionArguments.Any())
            {                
                var actionParametersWithRequiredAttribute =
                    actionParameters.Where(p => p.GetCustomAttributes<RequiredAttribute>().Any());

                foreach (var actionParameterWithRequiredAttribute in actionParametersWithRequiredAttribute)
                {
                    var actionArgument =
                        actionContext.ActionArguments.Where(
                                a => a.Key == actionParameterWithRequiredAttribute.ParameterName)
                            .Select(a => new { a.Key, a.Value })
                            .FirstOrDefault();

                    if (actionArgument == null)
                    {
                        throw new InvalidOperationException(
                            $"Parameter with Required attribute '{actionParameterWithRequiredAttribute.ParameterName}' not found in action arguments");
                    }

                    if (actionArgument.Value == null)
                    {
                        // TODO: review JSON vs XML
                        var jsonSerializerSettings = 
                            actionContext.Request?.GetConfiguration()?.Formatters?.JsonFormatter.SerializerSettings;

                        await
                            this.SetValidationResponse(
                                actionContext,
                                cancellationToken,
                                this.ToStandardRequestBodyMissingError(jsonSerializerSettings, actionParameterWithRequiredAttribute));
                    }
                }
            }
        }

        private ValidationErrorDetails ToStandardModelStateResponse(
            ModelStateDictionary modelState,
            IEnumerable<HttpParameterDescriptor> actionParameters)
        {
            return modelState.ToValidationErrorDetails("There was a validation error.", "resource");
        }
    
        private ValidationErrorDetails ToStandardRequestBodyMissingError(
            JsonSerializerSettings settings,
            HttpParameterDescriptor parameter)
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

        private async Task SetValidationResponse(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            ValidationErrorDetails validationErrorDetails)
        {
            var result = new ResourceValidationResult(actionContext.Request, validationErrorDetails);
            var response = await result.ExecuteAsync(cancellationToken);
            actionContext.Response = response;
        }
    }
}
