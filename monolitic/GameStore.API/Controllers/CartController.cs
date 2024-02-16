using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API;


[Authorize]
[ApiController]
[Route("api/catalog")]
public class CartController() : ControllerBase
{

}
