namespace AutoResponse.Sample.Domain.Exceptions
{
    using System;

    public class DomainValidationException : Exception
    {
        public DomainValidationException(string message)
        {
            this.Event = new DomainValidationApiEvent(message);
        }

        public DomainValidationApiEvent Event { get; }
    }
}
