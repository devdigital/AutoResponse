namespace AutoResponse.Core.Logging
{
    using System;
    using System.Threading.Tasks;

    public class NullAutoResponseLogger : IAutoResponseLogger
    {
        public Task LogException(Exception exception)
        {
            return Task.FromResult(0);
        }
    }
}