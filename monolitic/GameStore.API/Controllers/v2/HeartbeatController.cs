using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.v2;

[ApiController]
[ApiVersion("1.1")]
// [Route("api/v{version:apiVersion}/heartbeat")]
[Route("api/heartbeat")]
public class HeartbeatController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.1")]
    public ActionResult Get()
    {
        return Ok(DateTime.Now.ToString("zz"));
    }
}
