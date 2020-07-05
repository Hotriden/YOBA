using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;
using YOBA_Web.Areas.Identity.Data;
using YOBA_Web.Models;

namespace YOBA_Web.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly string _webServerAddres;

        public RegisterController(
            UserManager<IdentityUser> userManager,
            IEmailService emailService,
            IConfiguration config)
        {
            _userManager = userManager;
            _emailService = emailService;
            _webServerAddres = config.GetSection("Web_UI").GetSection("Server").Value;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Post(UserModel identityUser)
        {
            if (Verification.VerifyEmail(identityUser.Email) == false)
            {
                return StatusCode(409, "Incorrect mail address");
            }

            var findEmail = _userManager.FindByEmailAsync(identityUser.Email).Result;
            if (findEmail != null)
            {
                return StatusCode(409, "User already exist. Wanna recover your account?");
            }

            var user = new IdentityUser
            {
                UserName = identityUser.FirstName,
                Email = identityUser.Email,
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(VerifyEmail), "Register", new { userId = user.Id, code }, Request.Scheme, Request.Host.ToString());
                await _emailService.SendAsync(identityUser.Email, "Account Confirmation", Verification.VerificationMessage(identityUser.FirstName, link), true);
                return StatusCode(200, $"Check {user.Email} to verificate your account");
            }
            return StatusCode(400, "Some Thing Gone Wrong");
        }

        public async Task<ActionResult> VerifyEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return StatusCode(400, "User not found");
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Redirect("https://yoba.netlify.app/Verify");
            }
            return Redirect("https://yoba.netlify.app/VerifyError");
        }
    }
}
