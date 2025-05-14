using Microsoft.AspNetCore.Mvc;
using POC___Middleware.Models;
using System.Linq;

namespace POC___Middleware.Controllers
{
    [ApiController]
    [Route("audit/logs")]
    public class AuditController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetLogs([FromQuery] string? method)
        {
            var logs = string.IsNullOrEmpty(method)
                ? AuditStore.Logs
                : AuditStore.Logs.Where(l => l.HTTPMethodName.Equals(method, StringComparison.OrdinalIgnoreCase));

            return Ok(logs);
        }
    }
}
