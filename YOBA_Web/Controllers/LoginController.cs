using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YOBA_Web.Areas.Identity.Data;

namespace YOBA_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;


        }
        public string Index()
        {
            return "Home page";
        }

        [Authorize]
        public string Secret()
        {
            return "This is secret just for autorized users";
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user == null)
            {
                return StatusCode(406, "User not found");
            }
            var password = await _userManager.CheckPasswordAsync(user, userModel.Password);
            if (password)
            {
                await _signInManager.SignInAsync(user, false);
            }
            return StatusCode(400, $"Validation error");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}