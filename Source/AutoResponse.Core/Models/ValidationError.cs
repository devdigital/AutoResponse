namespace AutoResponse.Core.Models
{
    using System;

    using AutoResponse.Core.Enums;

    public class ValidationError
    {
        public ValidationError(string resource, string field, ValidationErrorCode code, string errorMessage = null)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (string.IsNullOrWhiteSpace(field))
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (code == ValidationErrorCode.None)
            {
                throw new ArgumentNullException(nameof(code));
            }

            this.Resource = resource;
            this.Field = field;
            this.Code = code;
            this.Message = errorMessage;
        }        

        public string Resource { get; }

        public string Field { get; set; }

        public ValidationErrorCode Code { get; set; }

        public string Message { get; set; }
    }
}