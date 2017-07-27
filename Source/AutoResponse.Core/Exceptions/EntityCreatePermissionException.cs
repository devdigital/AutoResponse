namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityCreatePermissionException 
        : AutoResponseException
    {
        public EntityCreatePermissionException(string userId, string entityType) :
            base(new EntityCreatePermissionApiEvent(userId, entityType))
        {            
        }

        public EntityCreatePermissionException(string userId, string entityType, string entityId)
            : base(new EntityCreatePermissionApiEvent(userId, entityType, entityId))
        {
        }
    }
}