namespace AutoResponse.Data.Errors
{
    public enum EntityValidationErrorCode
    {
        None = 0,

        Missing,

        MissingField,

        Invalid,

        AlreadyExists
    }
}