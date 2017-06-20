namespace AutoResponse.Data.Exceptions
{
    public interface IEntityNotFoundException
    {
        string EntityType { get; }

        string EntityId { get; }
    }
}