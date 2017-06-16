namespace AutoResponse.WebApi2.Logging
{
    using System;

    public class NullEventLogger<TContext> : IEventLogger<TContext>
    {
        public void Trace<TEvent>(TEvent @event)
        {
        }

        public void Debug<TEvent>(TEvent @event)
        {            
        }

        public void Information<TEvent>(TEvent @event)
        {         
        }

        public void Warning<TEvent>(Exception exception, TEvent @event)
        {         
        }

        public void Error<TEvent>(Exception exception, TEvent @event)
        {            
        }
    }
}