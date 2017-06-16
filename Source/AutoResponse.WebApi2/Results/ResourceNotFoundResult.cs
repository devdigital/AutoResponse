namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    public class ResourceNotFoundResult : ErrorResult
    {
        private readonly string resourceType;

        private readonly string resourceId;

        public ResourceNotFoundResult(HttpRequestMessage request, string resourceType, string resourceId)
            : base(request, HttpStatusCode.NotFound)
        {
            if (string.IsNullOrWhiteSpace(resourceType))
            {
                throw new ArgumentNullException(nameof(resourceType));
            }

            if (string.IsNullOrWhiteSpace(nameof(resourceId)))
            {
                throw new ArgumentNullException(nameof(resourceId));
            }

            this.resourceType = resourceType;
            this.resourceId = resourceId;
        }

        protected override ValidationErrorDetails GetErrorDetails()
        {
            return new ValidationErrorDetails(
                $"The {this.resourceType} resource with identifier '{this.resourceId}' was not found.",
                new List<ValidationError>
                {
                    new ValidationError(this.resourceType, "id", ValidationErrorCode.Missing)
                });
        }
    }

    public enum ValidationErrorCode
    {
        None = 0,

        Missing,

        MissingField,

        Invalid
    }
}