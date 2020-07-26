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
using YOBA_Web.Filters;

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

        [HttpGet("GetAll")]
        public ActionResult<List<WareHouse>> GetAll()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _logger.LogInformation("Log message in the GetAll() method");
            var result = _db.WareHouseRepository.GetAll(userId).ToList();
            return result;
        }

        [HttpGet("Get/{id}")]
        public ActionResult<WareHouse> Get(int id)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _db.WareHouseRepository.Get(userId, new WareHouse() { Id = id });
            if (result != null)
            {
                _logger.LogInformation($"INFO: {DateTime.Now} {GetType()} Success");
                return result;
            }
            else
            {
                return StatusCode(404, "WareHouse not founded");
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<WareHouse>> Post(WareHouse wareHouse)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (wareHouse.WareHouseName == null)
            {
                return BadRequest();
            }
            if (userId != null)
            {
                wareHouse.UserId = userId;
                await _db.WareHouseRepository.Add(userId, wareHouse);
            }
            return Ok(wareHouse);
        }


        [HttpPut("Change")]
        public async Task<ActionResult<WareHouse>> Change(WareHouse wareHouse)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (wareHouse == null)
            {
                return BadRequest();
            }
            if (_db.WareHouseRepository.Get(userId, wareHouse)==null)
            {
                return NotFound();
            }
            await _db.WareHouseRepository.Change(userId, wareHouse);
            return Ok(wareHouse);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<WareHouse>> Delete(WareHouse wareHouse)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            WareHouse wH = _db.WareHouseRepository.Get(userId, wareHouse);
            if (wH == null)
            {
                return NotFound();
            }
            await _db.WareHouseRepository.Delete(userId, wH);
            return Ok(wH);
        }
    }
}