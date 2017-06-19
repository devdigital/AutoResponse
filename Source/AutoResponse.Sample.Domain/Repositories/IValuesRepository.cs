namespace AutoResponse.Sample.Domain.Repositories
{
    using AutoResponse.Sample.Domain.Models;

    public interface IValuesRepository
    {
        Value GetValue(int valueId);
    }
}

