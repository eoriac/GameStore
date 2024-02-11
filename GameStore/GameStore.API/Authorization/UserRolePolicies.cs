using Microsoft.AspNetCore.Authorization;

namespace GameStore.API;

/// <summary>
/// User Policies
/// </summary>
public static class UserRolePolicies
{
    /// <summary>
    /// Policy to allow get items from library
    /// </summary>
    /// <returns></returns>
    public static AuthorizationPolicy CanGetLibraryGame(){
        return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireRole("NormalUser")
            .Build();        
    }

    /// <summary>
    /// Authorization policy to allow the creation of a Game
    /// </summary>
    /// <returns>Policy</returns>
    public static AuthorizationPolicy CanCreateLibraryGame(){
        return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim("company", "Steam")
            .Build();        
    }
}
