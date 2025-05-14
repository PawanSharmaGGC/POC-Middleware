namespace POC___Middleware.Models
{
    public class Logs
    {
        public DateTime Timestamp { get ; set; }
        public string RequestedURL { get; set; }
        public string HTTPMethodName { get; set; }
        public string RequestBody { get; set; }
        public string Headers { get; set; }
    }
}
