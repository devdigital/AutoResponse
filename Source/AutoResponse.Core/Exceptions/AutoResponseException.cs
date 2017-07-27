namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.ApiEvents;

    public class AutoResponseException : Exception
    {
        public AutoResponseException(AutoResponseApiEvent apiEvent)
        {
            if (apiEvent == null)
            {
                throw new ArgumentNullException(nameof(apiEvent));
            }

            this.Event = apiEvent;
        }

        public AutoResponseApiEvent Event { get; }
    }
}