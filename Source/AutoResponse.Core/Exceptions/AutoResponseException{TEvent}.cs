using AutoResponse.Core.ApiEvents;

namespace AutoResponse.Core.Exceptions
{
    public abstract class AutoResponseException<TEvent> : AutoResponseException where TEvent : IAutoResponseApiEvent
    {
        protected AutoResponseException(string message, TEvent apiEvent) : base(message, apiEvent)
        {            
            this.Event = apiEvent;
        }

        public TEvent Event { get; }
    }
}