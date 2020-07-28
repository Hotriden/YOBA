using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using YOBA_LibraryData.DAL.Entities.User;
using YOBA_Web.Models.JwtAuth;

namespace YOBA_Web.Controllers
{
    /// <summary>
    /// Authentication controller
    /// for give JWT token in sign in
    /// method 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public LoginController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger<LoginController>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SignIn")]
        public async Task<ActionResult> Login(UserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                var jwt = new JwtService(_config);
                var token = jwt.GenerateSecurityToken(user.Email, user.Id);
                _logger.LogInformation($"{DateTime.Now} - INFO: Signed In. UserId: {user.Id}.");
                return StatusCode(200, token);
            }
            return StatusCode(401, "Wrong email or password");
        }

        /// <summary>
        /// Created for make JWT token
        /// unavailable.
        /// Could be used just clearing cookies
        /// on client side
        /// </summary>
        /// <returns></returns>
        [HttpPost("SignOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        /// <summary>
        /// WebUi method for identify user
        /// by taken JWT token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUser()
        {
            string userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            return StatusCode(200, user.UserName);
        }
    }
}