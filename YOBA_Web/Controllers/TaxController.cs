using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace YOBA_Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : Controller
    {
        private IUnitOfWork _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaxController(
            IUnitOfWork db,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tax>>> Get()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _db.TaxRepository.GetAll(userId).ToList();
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<Tax>> Get(Tax _tax)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Tax tax = _db.TaxRepository.Get(userId, _tax);
            if (tax == null)
                return NotFound();
            return new ObjectResult(tax);
        }

        [HttpPost]
        public async Task<ActionResult<Tax>> Post(Tax tax)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (tax == null)
            {
                return BadRequest();
            }

            await _db.TaxRepository.Add(userId, tax);
            return Ok(tax);
        }

        [HttpPut]
        public async Task<ActionResult<Tax>> Put(Tax tax)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (tax == null)
            {
                return BadRequest();
            }
            if (_db.TaxRepository.Get(userId, tax) == null)
            {
                return NotFound();
            }

            await _db.TaxRepository.Change(userId, tax);
            return Ok(tax);
        }

        // DELETE api/users/5
        [HttpDelete("{id?}")]
        public async Task<ActionResult<Tax>> Delete(Tax _tax)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Tax tax = _db.TaxRepository.Get(userId, _tax);
            if (tax == null)
            {
                return NotFound();
            }
            await _db.TaxRepository.Delete(userId, tax);
            return Ok(tax);
        }
        }
}