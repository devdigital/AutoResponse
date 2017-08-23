namespace AutoResponse.Core.Dtos
{
    public class ResourceNotFoundApiModel : ErrorApiModel
    {
        public string Resource { get; set; }

        public string ResourceId { get; set; }
    }
}