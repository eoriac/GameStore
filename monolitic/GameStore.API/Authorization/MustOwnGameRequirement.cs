using Microsoft.AspNetCore.Authorization;

namespace GameStore.API;

public class MustOwnGameRequirement : IAuthorizationRequirement
{
    public MustOwnGameRequirement()
    {
        
    }
}