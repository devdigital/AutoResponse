namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityNotFoundException : AutoResponseException<EntityNotFoundApiEvent>
    {
        public EntityNotFoundException(string code, string entityType, string entityId)
            : base($"The entity {entityType} was not found with entity id {entityId}", new EntityNotFoundApiEvent(code, entityType, entityId))
        {
        }        

        public EntityNotFoundException(string entityType, string entityId)
            : base($"The entity {entityType} was not found with entity id {entityId}", new EntityNotFoundApiEvent(entityType, entityId))
        {            
        }     
    }
}
