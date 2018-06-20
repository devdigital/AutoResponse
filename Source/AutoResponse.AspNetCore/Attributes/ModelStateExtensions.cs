using System;
using System.Collections.Generic;
using System.Linq;
using AutoResponse.Core.Enums;
using AutoResponse.Core.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AutoResponse.AspNetCore.Attributes
{
    /// <summary>
    /// Model state extensions.
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// To the validation error details.
        /// </summary>
        /// <param name="modelStateDictionary">The model state dictionary.</param>
        /// <param name="message">The message.</param>
        /// <param name="resource">The resource.</param>
        /// <returns>The validation error details.</returns>
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
                        ToField(erroredModelState.Key) ?? "field",
                        ToValidationErrorCode(error),
                        error.ErrorMessage));
                }
            }

            return new ValidationErrorDetails(message, validationErrors);
        }

        private static string ToField(string modelStateKey)
        {
            if (string.IsNullOrWhiteSpace(modelStateKey))
            {
                return null;
            }

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