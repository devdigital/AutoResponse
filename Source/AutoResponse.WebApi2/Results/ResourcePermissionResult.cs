namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Net;
    using System.Net.Http;

    using AutoResponse.Core.Models;

    public class ResourcePermissionResult : ErrorActionResult
    {
        private readonly string userId;

        private readonly string resourceType;

        private readonly string resourceId;

        public ResourcePermissionResult(HttpRequestMessage request, string userId, string resourceType, string resourceId)
            : base(request, HttpStatusCode.Forbidden)
        {
            if (string.IsNullOrWhiteSpace(nameof(userId)))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(nameof(resourceType)))
            {
                throw new ArgumentNullException(nameof(resourceType));
            }

            if (string.IsNullOrWhiteSpace(nameof(resourceId)))
            {
                throw new ArgumentNullException(nameof(resourceId));
            }

            this.userId = userId;
            this.resourceType = resourceType;
            this.resourceId = resourceId;
        }

        protected override ValidationErrorDetails GetErrorDetails()
        {
            return new ValidationErrorDetails(
                $"The user with identifier '{this.userId}', does not have permission to access the {this.resourceType} resource with identifier '{this.resourceId}'");
        }
    }
}