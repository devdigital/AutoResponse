namespace AutoResponse.Core.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityValidationErrorDetails
    {
        public EntityValidationErrorDetails(string message) 
            : this(message, Enumerable.Empty<EntityValidationError>())
        {
        }

        public EntityValidationErrorDetails(string message, EntityValidationError error) 
            : this(message, new List<EntityValidationError> { error })
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }
        }

        public EntityValidationErrorDetails(string message, IEnumerable<EntityValidationError> errors)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            this.Message = message;
            this.Errors = errors;
        }

        public string Message { get; }

        public IEnumerable<EntityValidationError> Errors { get; }
    }
}