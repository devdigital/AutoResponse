namespace AutoResponse.Core.Dtos
{
    public class ResourceNotFoundDto : ErrorDto
    {
        public string Resource { get; set; }

        public string ResourceId { get; set; }
    }
}