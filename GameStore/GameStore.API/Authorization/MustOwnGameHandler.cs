using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.API;

public class MustOwnGameHandler(
    IGameLibraryRepository gameLibraryRepository,
    IHttpContextAccessor httpContextAccessor,
    ILogger<MustOwnGameHandler> logger
    ) : AuthorizationHandler<MustOwnGameRequirement>
{
    private readonly IGameLibraryRepository gameLibraryRepository = gameLibraryRepository;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly ILogger<MustOwnGameHandler> logger = logger;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnGameRequirement requirement)
    {
        var routeUserId = httpContextAccessor.HttpContext.GetRouteValue("userId")?.ToString();

        logger.LogDebug("Get route user " + routeUserId);

        // var userId = context.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        var userClaimId = context.User.Claims.FirstOrDefault(cm => cm.Type == "sub")?.Value;

        logger.LogDebug("Get user claim " + userClaimId);

        if (routeUserId != userClaimId){
            context.Fail();
            logger.LogDebug("UserClaim <> RouteUser");
            return;            
        }

        // 

        var routeGameId = httpContextAccessor.HttpContext.GetRouteValue("id")?.ToString();

        // IsUserGameOwner(routeUserId, routeGameId)       
    }
}
