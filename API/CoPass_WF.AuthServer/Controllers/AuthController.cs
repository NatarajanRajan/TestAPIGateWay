using CoPass_WF.AuthServer.API.Model;
using CoPass_WF.AuthServer.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CoPass_WF.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;
        private readonly ISqlServerConnectionProvider _provider;
        private readonly IConfiguration _configuration;  
        public AuthController(IJWTAuthenticationManager jWTAuthenticationManager, ISqlServerConnectionProvider provider, IConfiguration configuration)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
            this._provider = provider;
            this._configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Auth service is running");
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] UserModel userCred)
        {
            var reponse = jWTAuthenticationManager.AuthenticateAsync(userCred.username, userCred.password, _provider);
            if (!reponse.Result.status)
                return Unauthorized();

            return Ok(reponse.Result);
        }
        
    }
}
