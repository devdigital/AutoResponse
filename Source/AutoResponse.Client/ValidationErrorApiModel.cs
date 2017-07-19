namespace AutoResponse.Client
{
    public class ValidationErrorApiModel
    {
        public string Resource { get; set; }

        public string Field { get; set; }

        public string Code { get; set; }
        
        public string Message { get; set; }
    }
}