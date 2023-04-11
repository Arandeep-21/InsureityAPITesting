using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InsureityAPI.Controllers
{

    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> logger;
        public AuthController(IConfiguration config, ILogger<AuthController> _logger)
        {
            _config = config;
            logger = _logger;
        }


        public LoginResponse Login(LoginDTO login)
        {
            logger.LogInformation("Login response");
            var tokenString = GenerateJSONWebToken(login);
            var response = new LoginResponse { token = tokenString, UserEmail = login.UserEmail, Role = login.Role };
            return response;
        }

        private string GenerateJSONWebToken(LoginDTO userInfo)
        {
            logger.LogInformation("Generating JSON Web Token");

            if (userInfo is null)
            {
                throw new ArgumentNullException(nameof(userInfo));
            }
            List<Claim> claims = new List<Claim>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            claims.Add(new Claim("UserEmail", userInfo.UserEmail));
            if (userInfo.Role == "Agent")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Agent"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
