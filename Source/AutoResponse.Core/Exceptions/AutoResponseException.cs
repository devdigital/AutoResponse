using AutoResponse.Core.ApiEvents;

namespace AutoResponse.Core.Exceptions
{
    using System;

    public class AutoResponseException : Exception
    {
        public AutoResponseException(string message, IAutoResponseApiEvent apiEvent) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                
                throw new ArgumentNullException(nameof(message));
            }

            if (apiEvent == null)
            {
                throw new ArgumentNullException(nameof(apiEvent));
            }

            this.Event = apiEvent;
        }

        public IAutoResponseApiEvent Event { get; }
    }
}