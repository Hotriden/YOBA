using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace YOBA_Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : Controller
    {
        private IUnitOfWork _db;
        private readonly UserManager<IdentityUser> _userManager;

        public TaxController(
            IUnitOfWork db,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        public async Task<ActionResult<List<Tax>>> Get()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            return _db.TaxRepository.GetAll(userId).ToList();
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<Tax>> Get(int id)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            Tax tax = _db.TaxRepository.GetById(userId, id);
            if (tax == null)
                return NotFound();
            return new ObjectResult(tax);
        }

        [HttpPost]
        public async Task<ActionResult<Tax>> Post(Tax tax)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
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
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            if (tax == null)
            {
                return BadRequest();
            }
            if (_db.TaxRepository.GetById(userId, tax.Id) == null)
            {
                return NotFound();
            }

            await _db.TaxRepository.Change(userId, tax);
            return Ok(tax);
        }

        // DELETE api/users/5
        [HttpDelete("{id?}")]
        public async Task<ActionResult<Tax>> Delete(int id)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            Tax tax = _db.TaxRepository.GetById(userId, id);
            if (tax == null)
            {
                return NotFound();
            }
            await _db.TaxRepository.Delete(userId, tax);
            return Ok(tax);
        }
        }
}