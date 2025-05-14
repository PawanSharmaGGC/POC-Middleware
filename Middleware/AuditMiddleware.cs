using POC___Middleware.Models;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace POC___Middleware.Middleware
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering(); // Enable rewinding

            var bodyStream = new StreamReader(context.Request.Body);
            string bodyText = await bodyStream.ReadToEndAsync();

            context.Request.Body.Position = 0; // Rewind for next middleware

            var logs = new Logs
            {
                RequestedURL = context.Request.Path,
                HTTPMethodName = context.Request.Method,
                Timestamp = DateTime.Now,
                RequestBody = MaskSensitiveData(bodyText),
                Headers = context.Request.Headers["User-Agent"]
            };

            AuditStore.Logs.Add(logs);

            await _next(context);
        }
        private string MaskSensitiveData(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return json;

            return System.Text.RegularExpressions.Regex.Replace(
                json,
                "\"password\"\\s*:\\s*\"(.*?)\"",
                "\"password\":\"***\"",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
            );
        }
    }
}
