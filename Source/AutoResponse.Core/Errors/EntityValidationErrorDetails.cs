namespace AutoResponse.Core.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityValidationErrorDetails
    {
        public EntityValidationErrorDetails(string message, IEnumerable<EntityValidationError> errors = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Message = message;
            this.Errors = errors ?? Enumerable.Empty<EntityValidationError>();
        }

        public string Message { get; }

        public IEnumerable<EntityValidationError> Errors { get; }
    }
}