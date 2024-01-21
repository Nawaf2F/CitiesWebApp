using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApp.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configration;

        public class AuthenticationRequestBody 
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }

        private class CityInfoUser
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }

            public CityInfoUser(int userId,string userName,string firstName,string lastName,string city)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                City = city;
            }
        }
       
        public AuthenticationController(IConfiguration configuration) 
        { 
            _configration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            //1. Validate credentials
            var user = ValidateUserCredentials(
                authenticationRequestBody.UserName, 
                authenticationRequestBody.Password);

            if(user == null) 
            {
                return Unauthorized();
            }

            //2. Create a token
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            //3. The claims
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub",user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim ("family_name", user.LastName));
            claimsForToken.Add(new Claim("city", user.City));

            var jwtSecurityToken = new JwtSecurityToken(
                _configration["Authentication:Issuer"],
                _configration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private CityInfoUser ValidateUserCredentials(string? username, string? password)
        {
            // for this demo we assume the credentials are valid

            return new CityInfoUser(1, username ?? "", "Nawaf", "Ishaq", "Riyadh");

        }
    }
}
