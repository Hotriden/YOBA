using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;
using System.Text;
using System.Threading.Tasks;
using YOBA_Web.Models;
using YOBA_Web.Models.JwtAuth;

namespace YOBA_Web.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RecoverController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly string _webServerAddress;
        private readonly string _webSiteAddress;

        public RecoverController(
            UserManager<IdentityUser> userManager,
            IEmailService emailService,
            IConfiguration config)
        {
            _userManager = userManager;
            _emailService = emailService;
            _webServerAddress = config.GetSection("Web_UI").GetSection("Server").Value;
            _webSiteAddress = config.GetSection("WEB_UI").GetSection("WebSite").Value;
        }

        [HttpPost("Recover")]
        public async Task<ActionResult> Recover(ForgotPasswordModel model)
        {
            if (Verification.VerifyEmail(model.Email) == false)
            {
                return StatusCode(409, "Incorrect mail address");
            }

            var findEmail = _userManager.FindByEmailAsync(model.Email).Result;

            if (findEmail != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(findEmail);
                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);

                string url = _webSiteAddress + $"/CreatePassword/'{model.Email}'{validToken}";

                await _emailService.SendAsync(findEmail.Email, "Recover password", 
                    Verification.RecoverMessage(findEmail.UserName, url), true);
                return StatusCode(200, $"On {findEmail.Email} was send letter for recover your password.");
            }

            return StatusCode(400, "SomeThingGoneWrong");
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (Verification.VerifyEmail(model.Email) == false)
            {
                return StatusCode(409, "Incorrect mail address");
            }

            var user = _userManager.FindByEmailAsync(model.Email).Result;

            if (user != null)
            {
                var verify = _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", model.Token);
                if (verify.Result)
                {
                    var code = await _userManager.ResetPasswordAsync(user, model.Token, model.ConfirmPassword);
                    if (code.Succeeded)
                    {
                        return StatusCode(200, $"{user.UserName} your password was changed. Now you can log in.");
                    }
                }
                return StatusCode(200, "You changed your password by this link. Try again");
            }
            return StatusCode(400, "Something gone wrong");
        }
    }
}
