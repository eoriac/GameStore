using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/heartbeat")]
// [Route("api/v{version:apiVersion}/heartbeat")]
public class HeartbeatController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public string Get()
    {
        return DateTime.Now.ToString();
    }
}
