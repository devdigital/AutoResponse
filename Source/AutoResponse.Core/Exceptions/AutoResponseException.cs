namespace AutoResponse.Core.Exceptions
{
    using System;

    public class AutoResponseException : Exception
    {
        public AutoResponseException(object apiEvent)
        {
            if (apiEvent == null)
            {
                throw new ArgumentNullException(nameof(apiEvent));
            }

            this.Event = apiEvent;
        }

        public object Event { get; }
    }
}