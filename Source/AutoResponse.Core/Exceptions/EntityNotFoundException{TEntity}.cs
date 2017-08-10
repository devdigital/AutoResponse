namespace AutoResponse.Core.Exceptions
{
    public class EntityNotFoundException<TEntity> : EntityNotFoundException
    {
        public EntityNotFoundException(string code, string entityId)
            : base(code, typeof(TEntity).Name, entityId)
        {            
        }

        public EntityNotFoundException(string entityId)
            : base(typeof(TEntity).Name, entityId)
        {
        }
    }
}