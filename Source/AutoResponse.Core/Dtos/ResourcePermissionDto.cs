namespace AutoResponse.Core.Dtos
{
    public class ResourcePermissionDto : ErrorDto
    {
        public string UserId { get; set; }

        public string Resource { get; set; }

        public string ResourceId { get; set; }
    }
}