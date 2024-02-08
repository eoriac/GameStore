using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/heartbeat")]
// [Route("api/v{version:apiVersion}/heartbeat")]
public class HeartbeatController : ControllerBase
{
    private readonly ILogger<HeartbeatController> logger;

    public HeartbeatController(ILogger<HeartbeatController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public string Get()
    {
        logger.LogDebug("Entering heartbeat: " + Request.HttpContext.TraceIdentifier);
        return DateTime.Now.ToString();
    }
}
