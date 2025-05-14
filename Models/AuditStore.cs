using System.Collections.Concurrent;
namespace POC___Middleware.Models
{
    public class AuditStore
    {
        public static ConcurrentBag<Logs> Logs = new ConcurrentBag<Logs>();
    }
}
