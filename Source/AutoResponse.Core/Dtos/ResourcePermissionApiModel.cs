namespace AutoResponse.Core.Dtos
{
    public class ResourcePermissionApiModel : ErrorApiModel
    {
        public string UserId { get; set; }

        public string Resource { get; set; }

        public string ResourceId { get; set; }
    }
}