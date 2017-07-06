namespace AutoResponse.WebApi2.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.ModelBinding;

    using AutoResponse.Core;
    using AutoResponse.Core.Models;
    using AutoResponse.WebApi2.Results;

    using Humanizer;

    public static class ModelStateExtensions
    {
        public static ValidationErrorDetails ToValidationErrorDetails(
            this ModelStateDictionary modelStateDictionary,
            string message,
            string resource)
        {
            if (modelStateDictionary == null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentNullException(nameof(resource));
            }

            var erroredModelStates = modelStateDictionary.Where(m => m.Value.Errors.Any()).ToList();

            var validationErrors = new List<ValidationError>();
            foreach (var erroredModelState in erroredModelStates)
            {
                foreach (var error in erroredModelState.Value.Errors)
                {
                    validationErrors.Add(new ValidationError(
                        resource,
                        ToField(erroredModelState.Key), 
                        ToValidationErrorCode(error),
                        error.ErrorMessage));
                }                
            }

            return new ValidationErrorDetails(message, validationErrors);
        }

        private static string ToField(string modelStateKey)
        {
            var lastPeriodIndex = modelStateKey.LastIndexOf(".", StringComparison.Ordinal);
            var fieldName = lastPeriodIndex == -1 ? modelStateKey : modelStateKey.Substring(lastPeriodIndex + 1);
            return fieldName.Camelize();
        }

        private static ValidationErrorCode ToValidationErrorCode(ModelError error)
        {
            if (error.Exception != null)
            {
                return ValidationErrorCode.Invalid;
            }

            if (string.IsNullOrWhiteSpace(error.ErrorMessage))
            {
                return ValidationErrorCode.Invalid;
            }

            if (error.ErrorMessage.Contains("required"))
            {
                return ValidationErrorCode.MissingField;
            }            

            return ValidationErrorCode.Invalid;
        }
    }
}