namespace AutoResponse.Core.ApiEvents
{
    using System;

    public class UnauthenticatedApiEvent : IAutoResponseApiEvent
    {
        public UnauthenticatedApiEvent(string code, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Code = code;
            this.Message = message;
        }

        public UnauthenticatedApiEvent(string message) : this("AR401", message)
        {
        }

        public string Code { get; }

        public string Message { get; }
    }
}