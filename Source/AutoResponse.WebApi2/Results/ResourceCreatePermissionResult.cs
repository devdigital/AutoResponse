namespace AutoResponse.WebApi2.Results
{
    using System;
    using System.Net;
    using System.Net.Http;

    public class ResourceCreatePermissionResult : ErrorResult
    {
        private readonly string userId;

        private readonly string resourceType;

        public ResourceCreatePermissionResult(HttpRequestMessage request, string userId, string resourceType)
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

        protected override ValidationErrorDetails GetErrorDetails()
        {
            return new ValidationErrorDetails(
                $"The user with identifier '{this.userId}', does not have permission to create a {this.resourceType} resource");
        }
    }
}