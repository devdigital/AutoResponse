namespace AutoResponse.Sample.Domain.Exceptions
{
    public class DomainValidationApiEvent
    {
        public DomainValidationApiEvent(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }
}