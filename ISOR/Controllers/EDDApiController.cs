using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ISOR.Controllers
{
    [ApiController]
    [Route("api/edd")]
    public class EDDApiController : Controller
    {
        IConfiguration config;

        public EDDApiController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet("test")]
        public ActionResult<IEnumerable<string>> GetTest(string test)
        {
            return new List<string>() { "asdf", "1234", "test", test };
        }

        [HttpGet("admin")]
        [Authorize("OnlyAdmin")]
        public ActionResult<bool> OnlyAdmin()
        {
            return true;
        }

        [HttpGet("getToken")]
        public ActionResult<string> GetToken(string username, string password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new(ClaimTypes.NameIdentifier, username),
                new(ClaimTypes.Role, password)
            };
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
