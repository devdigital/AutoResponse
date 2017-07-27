namespace AutoResponse.Core.Logging
{
    using System;
    using System.Threading.Tasks;

    public interface IAutoResponseLogger
    {
        Task LogException(Exception exception);
    }
}
