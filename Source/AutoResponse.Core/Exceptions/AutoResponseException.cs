namespace AutoResponse.Core.Exceptions
{
    using System;

    public abstract class AutoResponseException : Exception
    {
        protected AutoResponseException(string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
        }

        public abstract object EventObject { get; }
    }
}