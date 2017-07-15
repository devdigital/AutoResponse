namespace AutoResponse.Core.Exceptions
{
    public interface IEntityNotFoundException
    {
        string EntityType { get; }

        string EntityId { get; }
    }
}