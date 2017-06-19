namespace AutoResponse.Data.Exceptions
{
    public class EntityPermissionException<TEntity> : EntityPermissionException
    {
        public EntityPermissionException(string userId)
            : base(userId, typeof(TEntity).Name)
        {
        }

        public EntityPermissionException(string userId, string entityId)
            : base(userId, typeof(TEntity).Name, entityId)
        {
        }
    }
}