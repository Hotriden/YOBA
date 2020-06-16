﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YOBA_Web.Areas.Identity.Data;
using YOBA_Web.Models.JwtAuth;

namespace YOBA_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Index()
        {
            return "Home page";
        }


        [HttpPost("SignIn")]
        public async Task<ActionResult> Login(UserModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user == null)
            {
                return StatusCode(406, "User not found");
            }
            var result = await _signInManager.PasswordSignInAsync(user, userModel.Password, false, false);
            if (result.Succeeded)
            {
                var jwt = new JwtService(_config);
                var token = jwt.GenerateSecurityToken(userModel.Email);
                return StatusCode(200, token);
            }
            return StatusCode(401, "Wrong email or password");
            
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUser()
        {
            string userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            return StatusCode(200, user);
        }
    }
}