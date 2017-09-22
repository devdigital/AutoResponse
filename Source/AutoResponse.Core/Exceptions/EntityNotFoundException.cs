namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityNotFoundException : AutoResponseException<EntityNotFoundApiEvent>
    {
        public EntityNotFoundException(string code, string entityType, string entityId)
            : base($"The {entityType} entity with identifier {entityId} was not found.", new EntityNotFoundApiEvent(code, entityType, entityId))
        {
        }        

        public EntityNotFoundException(string entityType, string entityId)
            : base($"The {entityType} entity with identifier {entityId} was not found.", new EntityNotFoundApiEvent(entityType, entityId))
        {            
        }     
    }
}
