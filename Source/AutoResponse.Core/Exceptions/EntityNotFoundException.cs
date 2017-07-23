namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.ApiEvents;

    public class EntityNotFoundException : AutoResponseException
    {
        public EntityNotFoundException(string entityType, string entityId)
            : base(new EntityNotFoundApiEvent(entityType, entityId))
        {            
        }     
    }
}
