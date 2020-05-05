using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YOBA_Web.Models.JwtAuth;

namespace YOBA_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController
    {
        private IConfiguration _config;

        public JwtController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public string GetRandomToken()
        {
            var jwt = new JwtService(_config);
            var token = jwt.GenerateSecurityToken("fake@email.com");
            return token;
        }
    }
}
