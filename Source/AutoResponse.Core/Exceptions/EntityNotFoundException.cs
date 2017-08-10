namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class EntityNotFoundException : AutoResponseException
    {
        public EntityNotFoundException(string code, string entityType, string entityId)
            : base(new EntityNotFoundApiEvent(code, entityType, entityId))
        {
        }        

        public EntityNotFoundException(string entityType, string entityId)
            : base(new EntityNotFoundApiEvent(entityType, entityId))
        {            
        }     
    }
}
