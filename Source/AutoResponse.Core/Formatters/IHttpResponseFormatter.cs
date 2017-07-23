namespace AutoResponse.Core.Formatters
{
    using System;

    using AutoResponse.Core.ApiEvents;

    public interface IHttpResponseFormatter
    {
        string EntityMessageToResourceMessage(string message);

        string EntityTypeToResource(string entityType);

        string EntityPropertyToField(string entityProperty);

        string ApiEventToCode(AutoResponseApiEvent apiEvent);
    }
}