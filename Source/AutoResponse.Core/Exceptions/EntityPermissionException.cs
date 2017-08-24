namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityPermissionException : AutoResponseException<EntityPermissionApiEvent>
    {
        public EntityPermissionException(string code, string userId, string entityType, string entityId)
            : base(new EntityPermissionApiEvent(code, userId, entityType, entityId))
        {            
        }

        public EntityPermissionException(string userId, string entityType, string entityId)
            : base(new EntityPermissionApiEvent(userId, entityType, entityId))
        {            
        }        
    }
}