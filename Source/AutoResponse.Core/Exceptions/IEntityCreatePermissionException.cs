namespace AutoResponse.Core.Exceptions
{
    public interface IEntityCreatePermissionException
    {
        string UserId { get; }

        string EntityType { get; }

        string EntityId { get; }
    }
}