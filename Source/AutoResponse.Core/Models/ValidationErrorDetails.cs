﻿namespace AutoResponse.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationErrorDetails
    {
        public ValidationErrorDetails(string message, IEnumerable<ValidationError> errors = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Message = message;
            this.Errors = errors ?? Enumerable.Empty<ValidationError>();
        }

        public string Message { get; }

        public IEnumerable<ValidationError> Errors { get; set; }
    }
}