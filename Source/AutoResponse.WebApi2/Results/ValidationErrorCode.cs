namespace AutoResponse.WebApi2.Results
{
    public enum ValidationErrorCode
    {
        None = 0,

        Missing,

        MissingField,

        Invalid,

        AlreadyExists
    }
}