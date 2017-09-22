namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityCreatePermissionException 
        : AutoResponseException<EntityCreatePermissionApiEvent>
    {
        public EntityCreatePermissionException(string code, string userId, string entityType, string entityId)
            : base($"The user with identifier '{userId}', does not have permission to create a {entityType} entity.", new EntityCreatePermissionApiEvent(code, userId, entityType, entityId))
        {
        }

        public EntityCreatePermissionException(string userId, string entityType) :
            base($"The user with identifier '{userId}', does not have permission to create a {entityType} entity.", new EntityCreatePermissionApiEvent(userId, entityType))
        {            
        }

        public EntityCreatePermissionException(string userId, string entityType, string entityId)
            : base($"The user with identifier '{userId}', does not have permission to create a {entityType} entity.", new EntityCreatePermissionApiEvent(userId, entityType, entityId))
        {
        }
    }
}