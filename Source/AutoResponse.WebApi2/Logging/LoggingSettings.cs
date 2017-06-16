namespace AutoResponse.WebApi2.Logging
{
    public class LoggingSettings
    {
        public bool Enabled { get; set; }

        public LogLevel Level { get; set; }

        public bool IncludeErrorDetailInResponse { get; set; }

        public bool IncludeWebApiTracing { get; set; }

        public bool IncludeInstrumentation { get; set; }

        public string LogFilePath { get; set; }        

        public string SeqUri { get; set; }
    }

    public class LogLevel
    {
    }
}