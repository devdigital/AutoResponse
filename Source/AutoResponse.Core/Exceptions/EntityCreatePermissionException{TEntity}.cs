namespace AutoResponse.Core.Exceptions
{
    public class EntityCreatePermissionException<TEntity> : EntityCreatePermissionException
    {
        public EntityCreatePermissionException(string userId)
            : base(userId, typeof(TEntity).Name)
        {
        }

        public EntityCreatePermissionException(string userId, string entityId)
            : base(userId, typeof(TEntity).Name, entityId)
        {
        }
    }
}