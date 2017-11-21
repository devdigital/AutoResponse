namespace AutoResponse.WebApi2.Extensions
{
    using System;

    using Newtonsoft.Json;

    public static class TypeExtensions
    {
        // TODO: currently uses a crude approach where an instance is serialised
        // so property values are null. Should use stubbing framework instead to generate instance with real looking stub data
        // Should also show (optional) for non required fields.
        public static string ToDescription(this Type type, JsonSerializerSettings settings)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            try
            {
                var instance = Activator.CreateInstance(type);
                return JsonConvert.SerializeObject(instance, Formatting.Indented, settings);
            }
            catch
            {
                return null;
            }
                                   
            // var publicAutoProperties =
            //    type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead && p.CanWrite);
        }
    }
}