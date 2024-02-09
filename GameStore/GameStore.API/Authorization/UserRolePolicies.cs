using Microsoft.AspNetCore.Authorization;

namespace GameStore.API;

public static class UserRolePolicies
{
    public static AuthorizationPolicy CanGetLibraryGame(){
        return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim("company", "Steam")
            .RequireRole("GoldUser")
            .Build();        
    }
}
