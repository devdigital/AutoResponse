namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.ApiEvents;

    public class EntityPermissionException : AutoResponseException
    {
        public EntityPermissionException(string message) : base(new EntityPermissionApiEvent(message))
        {    
        }

        public EntityPermissionException(string userId, string entityType, string entityId)
            : base(new EntityPermissionApiEvent(userId, entityType, entityId))
        {            
        }        
    }
}