using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace orderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly StaticUserOption staticUserOption;

        public AccountController(ILogger<AccountController> logger
            , IOptions<StaticUserOption> staticUserOption)
        {
            _logger = logger;
            this.staticUserOption = staticUserOption.Value;
        }

        [HttpPost("[action]")]
        public IActionResult Token(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (loginViewModel.UserName != staticUserOption.UserName)
            {
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Username is invalid");
                return BadRequest(ModelState);
            }

            if (loginViewModel.Password != staticUserOption.Password)
            {
                ModelState.AddModelError(nameof(LoginViewModel.Password), "Invalid credentials");
                return BadRequest(ModelState);
            }

            // Define the security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7HQ+fEys1LYTpFmi5vJbL+uwkEK15yupTq9TjmzVe/A="));

            // Define the signing credentials
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Define the claims (optional)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loginViewModel.UserName),  // Subject (e.g., user ID)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // Unique token ID
            };

            // Create the token
            var token = new JwtSecurityToken(
                issuer: "Web API",                // Issuer
                audience: "Next App",            // Audience
                claims: claims,                // Claims
                expires: DateTime.UtcNow.AddDays(1),  // Expiration time
                signingCredentials: signingCredentials  // Signing credentials
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new 
            {
                access_token = accessToken,
                type = "Bearer"
            });
        }
    }
}
