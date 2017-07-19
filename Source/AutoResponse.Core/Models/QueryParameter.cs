namespace AutoResponse.Core.Models
{
    using System;

    public class QueryParameter
    {
        public QueryParameter(string key) : this(key, value: null)
        {            
        }

        public QueryParameter(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            this.Key = key;
            this.Value = value;
        }        

        public string Key { get; }

        public string Value { get; }
    }
}