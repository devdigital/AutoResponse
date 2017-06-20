namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Net;
    using System.Net.Http;

    public class ResourceCreatePermissionResult : ErrorActionResult
    {
        private readonly string userId;

        private readonly string resourceType;

        private readonly string resourceId;

        public ResourceCreatePermissionResult(
            HttpRequestMessage request,
            string userId,
            string resourceType)
            : base(request, HttpStatusCode.Forbidden)
        {
            if (string.IsNullOrWhiteSpace(nameof(userId)))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(resourceType))
            {
                throw new ArgumentNullException(nameof(resourceType));
            }

            this.userId = userId;
            this.resourceType = resourceType;
        }

        public ResourceCreatePermissionResult(
            HttpRequestMessage request, 
            string userId, 
            string resourceType, 
            string resourceId)
            : base(request, HttpStatusCode.Forbidden)
        {
            if (string.IsNullOrWhiteSpace(nameof(userId)))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(resourceType))
            {
                throw new ArgumentNullException(nameof(resourceType));
            }

            if (string.IsNullOrWhiteSpace(resourceId))
            {
                throw new ArgumentNullException(nameof(resourceId));
            }

            this.userId = userId;
            this.resourceType = resourceType;
            this.resourceId = resourceId;
        }

        protected override ValidationErrorDetails GetErrorDetails()
        {
            if (string.IsNullOrWhiteSpace(this.resourceId))
            {
                return new ValidationErrorDetails(
                    $"The user with identifier '{this.userId}', does not have permission to create a {this.resourceType} resource");
            }

            return new ValidationErrorDetails(
                $"The user with identifier '{this.userId}', does not have permission to create a {this.resourceType} resource with resource identifier '{this.resourceId}'");
        }
    }
}