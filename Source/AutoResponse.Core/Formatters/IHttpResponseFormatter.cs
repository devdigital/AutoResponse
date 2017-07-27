namespace AutoResponse.Core.Formatters
{
    using System;

    using AutoResponse.Core.ApiEvents;

    public interface IHttpResponseFormatter
    {
        string Message(string message);

        string Resource(string entityType);

        string Field(string entityProperty);

        string ApiEventToCode(AutoResponseApiEvent apiEvent);
    }
}