using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using NETCore.MailKit.Core;
using System.Text;
using System.Threading.Tasks;
using YOBA_Web.Areas.Identity.Data;
using YOBA_Web.Models;

namespace YOBA_Web.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RecoverController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public RecoverController(
            UserManager<IdentityUser> userManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost("Recover")]
        public async Task<ActionResult> Recover(UserModel identityUser)
        {
            if (Verification.VerifyEmail(identityUser.Email) == false)
            {
                return StatusCode(409, "Incorrect mail address");
            }

            var findEmail = _userManager.FindByEmailAsync(identityUser.Email).Result;

            var user = new IdentityUser
            {
                UserName = identityUser.FirstName,
                Email = identityUser.Email,
            };

            if (findEmail != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);

                string url = "https://localhost:3000" + $"/Recover{validToken}";

                await _emailService.SendAsync(identityUser.Email, "Recover password", 
                    Verification.RecoverMessage(identityUser.FirstName, url), true);
                return StatusCode(200, $"On {identityUser.Email} was send letter for recover your password.");
            }

            return StatusCode(400, "SomeThingGoneWrong");
        }

        [HttpPost("Changepass")]
        public async Task<ActionResult> ChangePassword(UserModel identityUser)
        {
            if (Verification.VerifyEmail(identityUser.Email) == false)
            {
                return StatusCode(409, "Incorrect mail address");
            }

            var findEmail = _userManager.FindByEmailAsync(identityUser.Email).Result;

            var user = new IdentityUser
            {
                UserName = identityUser.FirstName,
                Email = identityUser.Email,
            };

            if (findEmail != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action(nameof(RecoverPassword), "Recover", new { userId = user.Id, code },
                    Request.Scheme, Request.Host.ToString());
                await _emailService.SendAsync(identityUser.Email, "Recover password",
                    Verification.RecoverMessage(identityUser.FirstName, link), true);
                return StatusCode(200, $"On {identityUser.Email} was send letter for recover your password.");
            }

            return StatusCode(400, "SomeThingGoneWrong");
        }

        public async Task<ActionResult> RecoverPassword(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return StatusCode(400, "User not found");
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return StatusCode(200, "User successful created");
            }
            return StatusCode(400, "SomeThingGoneWrong");
        }
    }
}
