namespace AutoResponse.Core.Mappers
{
    using System;

    public class MappingConfiguration
    {
        public MappingConfiguration(object context, IHttpResponseFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.Context = context;
            this.Formatter = formatter;
        }        

        public object Context { get; }

        public IHttpResponseFormatter Formatter { get; }
    }
}