using AutoResponse.Core.ApiEvents;

namespace AutoResponse.Core.Exceptions
{
    using System;

    public abstract class AutoResponseException : Exception
    {
        protected AutoResponseException(string message, IAutoResponseApiEvent apiEvent) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {

                throw new ArgumentNullException(nameof(message));
            }

            if (apiEvent == null)
            {
                throw new ArgumentNullException(nameof(apiEvent));
            }

            this.EventObject = apiEvent;
        }

        public object EventObject { get; }
    }
}