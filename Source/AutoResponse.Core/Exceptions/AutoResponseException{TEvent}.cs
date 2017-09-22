using System;

namespace AutoResponse.Core.Exceptions
{
    public abstract class AutoResponseException<TEvent> : AutoResponseException
    {
        protected AutoResponseException(string message, TEvent apiEvent) : base(message)
        {
            if (apiEvent == null)
            {
                throw new ArgumentNullException(nameof(apiEvent));
            }

            this.Event = apiEvent;
        }

        public TEvent Event { get; }

        public override object EventObject => this.Event;
    }
}