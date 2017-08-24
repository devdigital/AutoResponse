namespace AutoResponse.Core.Exceptions
{
    using System;

    public abstract class AutoResponseException : Exception
    {
        public abstract object EventObject { get; }
    }

    public abstract class AutoResponseException<TEvent> : AutoResponseException
    {
        protected AutoResponseException(TEvent apiEvent)
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