namespace AutoResponse.Owin
{
    using System;

    using AutoResponse.Core.Enums;
    using AutoResponse.Data.Errors;

    internal static class EntityValidationErrorCodeExtensions
    {
        public static ValidationErrorCode ToValidationErrorCode(this EntityValidationErrorCode errorCode)
        {
            switch (errorCode)
            {
                case EntityValidationErrorCode.None:
                    return ValidationErrorCode.None;
                case EntityValidationErrorCode.Missing:
                    return ValidationErrorCode.Missing;
                case EntityValidationErrorCode.MissingField:
                    return ValidationErrorCode.MissingField;
                case EntityValidationErrorCode.Invalid:
                    return ValidationErrorCode.Invalid;
                case EntityValidationErrorCode.AlreadyExists:
                    return ValidationErrorCode.AlreadyExists;
                default:
                    throw new ArgumentOutOfRangeException(nameof(errorCode), errorCode, null);
            }
        }
    }
}