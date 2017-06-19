namespace AutoResponse.Sample.Data.Repositories
{
    using AutoResponse.Data.Exceptions;
    using AutoResponse.Sample.Domain.Models;
    using AutoResponse.Sample.Domain.Repositories;

    public class DefaultValuesRepository : IValuesRepository
    {
        public Value GetValue(int valueId)
        {
            if (valueId == 1)
            {
                return new Value(1);
            }

            throw new EntityNotFoundException<Value>(valueId.ToString());
        }
    } 
}
