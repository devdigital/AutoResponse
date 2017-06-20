namespace AutoResponse.WebApi2.Results
{
    public class ErrorApiModel
    {
        public string Resource { get; set; }

        public string Field { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}