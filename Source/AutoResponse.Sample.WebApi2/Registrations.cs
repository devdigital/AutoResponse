namespace AutoResponse.Sample.WebApi2
{
    using System;
    using System.Collections.Generic;

    public class Registrations
    {        
        public Registrations(
            IDictionary<Type, Type> typeRegistrations, 
            IDictionary<Type, object> instanceRegistrations)
        {
            this.TypeRegistrations = typeRegistrations;
            this.InstanceRegistrations = instanceRegistrations;
        }

        public IDictionary<Type, Type> TypeRegistrations { get; }

        public IDictionary<Type, object> InstanceRegistrations { get; }
    }
}