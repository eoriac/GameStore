using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.API;

    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public class AuthenticationRequestBody
        {
            public string? UserId { get; set; }
            public string? Password { get; set; }
        }

        public AuthenticationController(
            IUserRepository userRepository,
            IConfiguration configuration
            )
        {
            this.userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));

            this.configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(
            AuthenticationRequestBody authenticationRequestBody)
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
                Encoding.ASCII.GetBytes("CTZAGTFA34d68C5PmduNwxKt3F6d9FjAWdgXCBAe6F8b32XT5G593mTAnWyjcLnK"));

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", "65c4aaa7f0f845d7419d3af0"),
                new Claim("name", user.Name),
                new Claim("email", user.Email),
                new Claim("company", "Steam"),
                new Claim("role", "GoldUser")
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
