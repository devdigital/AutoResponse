namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityNotFoundException : AutoResponseException
    {
        public EntityNotFoundException(string message) 
            : base(new EntityNotFoundApiEvent(message))
        {
        }

        public EntityNotFoundException(string entityType, string entityId)
            : base(new EntityNotFoundApiEvent(entityType, entityId))
        {            
        }     
    }
}
