namespace AutoResponse.Sample.Data.Repositories
{
    using AutoResponse.Sample.Domain.Models;
    using AutoResponse.Sample.Domain.Repositories;

    public class DefaultValuesRepository : IValuesRepository
    {
        public Value GetValue(int valueId)
        {
            return new Value(valueId);
        }
    } 
}
