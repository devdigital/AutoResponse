namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityPermissionException : AutoResponseException<EntityPermissionApiEvent>
    {
        public EntityPermissionException(string code, string userId, string entityType, string entityId)
            : base($"The user with identifier '{userId}', does not have permission to access the {entityType} entity with identifier '{entityId}'", new EntityPermissionApiEvent(code, userId, entityType, entityId))
        {            
        }

        public EntityPermissionException(string userId, string entityType, string entityId)
            : base($"The user with identifier '{userId}', does not have permission to access the {entityType} entity with identifier '{entityId}'", new EntityPermissionApiEvent(userId, entityType, entityId))
        {            
        }        
    }
}