using C__ASP.NET.models;
using Microsoft.AspNetCore.Mvc;

namespace C__ASP.NET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;

        public TokenController(ILogger<TokenController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ActionName("GetToken")]
        public JSONWebTokens Get([FromQuery(Name = "username")] string username)
        {
            return new JSONWebTokens(username);
        }
    }
}
