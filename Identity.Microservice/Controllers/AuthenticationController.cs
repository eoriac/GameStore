using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// 
/// </summary>
[Route("api/authentication")]
[ApiController]
public class AuthenticationController(
    IUserRepository userRepository,
    IConfiguration configuration
        ) : ControllerBase
{
    private readonly IUserRepository userRepository = userRepository ??
            throw new ArgumentNullException(nameof(userRepository));
    private readonly IConfiguration configuration = configuration ??
            throw new ArgumentNullException(nameof(configuration));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authenticationRequestBody"></param>
    /// <returns></returns>
    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> Authenticate(
        AuthenticationDto authenticationRequestBody)
    {
        var user = await ValidateUserCredentialsAsync(
            authenticationRequestBody.UserId,
            authenticationRequestBody.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        // Token generation
        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(configuration["Authentication:SecretForKey"] ?? "changeme"));

        var signingCredentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>
        {
            //new Claim("sub", "65c4aaa7f0f845d7419d3af0"),
            new Claim("sub", "65c617f0f5c5344f6db631f3"),
            new Claim("name", user.Name),
            new Claim("email", user.Email),
            new Claim("company", "Steam"),
            new Claim("role", "NormalUser")
        };

        var jwtSecurityToken = new JwtSecurityToken(
            configuration["Authentication:Issuer"],
            configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);

        return Ok(tokenToReturn);
    }

    private async Task<User> ValidateUserCredentialsAsync(string? userId, string? password)
    {
        //var user = await userRepository.GetUserAsync(Guid.Parse(userId), password);

        return new User()
        {
            Name = userId ?? "",
            Email = "email@email.com"
        };
    }
}
