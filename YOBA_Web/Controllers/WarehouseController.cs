using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/WareHouse")]
    public class WarehouseController : ControllerBase
    {
        private IUnitOfWork _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WarehouseController(
            IUnitOfWork db, 
            ILoggerFactory loggerFactory, 
            IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<WarehouseController>();
            _httpContextAccessor = httpContextAccessor;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<WareHouse>>> GetAll()
        {
            string userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            _logger.LogInformation("Log message in the GetAll() method");
            var result = _db.WareHouseRepository.GetAll(userId).ToList();
            return result;
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<WareHouse>> Get(WareHouse wareHouse)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            if (wareHouse == null)
            {
                _logger.LogInformation($"INFO: {DateTime.Now} {GetType()} Not Found");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"INFO: {DateTime.Now} {GetType()} Success");
                var result = _db.WareHouseRepository.GetWareHouseByUser(user);
                return result;
            }
        }

        [HttpPost("Post")]
        public async Task<ActionResult<WareHouse>> Post(WareHouse wareHouse)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            if (wareHouse.WareHouseName == null)
            {
                return BadRequest();
            }
            if (userId != null)
            {
                await _db.WareHouseRepository.Add(wareHouse);
            }
            return Ok(wareHouse);
        }


        [HttpPut("Put")]
        public async Task<ActionResult<WareHouse>> Put(WareHouse wareHouse)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            if (wareHouse == null)
            {
                return BadRequest();
            }
            if (_db.WareHouseRepository.GetById(userId, wareHouse.Id)==null)
            {
                return NotFound();
            }
            await _db.WareHouseRepository.Change(userId, wareHouse);
            return Ok(wareHouse);
        }

        [HttpDelete("{id?}")]
        public async Task<ActionResult<WareHouse>> Delete(int id)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            WareHouse wareHouse = _db.WareHouseRepository.GetById(userId, id);
            if (wareHouse == null)
            {
                return NotFound();
            }
            await _db.WareHouseRepository.Delete(userId, wareHouse);
            return Ok(wareHouse);
        }
    }
}