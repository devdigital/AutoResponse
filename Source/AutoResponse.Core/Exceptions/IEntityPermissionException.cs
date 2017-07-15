namespace AutoResponse.Core.Exceptions
{
    public interface IEntityPermissionException
    {
        string UserId { get; }

        string EntityType { get; }

        string EntityId { get; }
    }
}