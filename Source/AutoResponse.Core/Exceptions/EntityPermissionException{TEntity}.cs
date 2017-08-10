namespace AutoResponse.Core.Exceptions
{
    public class EntityPermissionException<TEntity> : EntityPermissionException
    {
        public EntityPermissionException(string code, string userId, string entityId)
            : base(code, userId, typeof(TEntity).Name, entityId)
        {            
        }

        public EntityPermissionException(string userId, string entityId)
            : base(userId, typeof(TEntity).Name, entityId)
        {
        }
    }
}