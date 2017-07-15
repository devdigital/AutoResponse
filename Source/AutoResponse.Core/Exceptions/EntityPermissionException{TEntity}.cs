namespace AutoResponse.Core.Exceptions
{
    public class EntityPermissionException<TEntity> : EntityPermissionException
    {
        public EntityPermissionException(string userId, string entityId)
            : base(userId, typeof(TEntity).Name, entityId)
        {
        }
    }
}