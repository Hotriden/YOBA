using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using YOBA_LibraryData.BLL.Interfaces;
using Microsoft.Extensions.Logging;
using YOBA_LibraryData.BLL.Entities.Supply;
using Microsoft.AspNetCore.Identity;

namespace YOBA_Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Supplier")]
    public class SupplierController : ControllerBase
    {
        private IUnitOfWork _db;
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        
        public SupplierController(
            IUnitOfWork db, 
            ILoggerFactory loggerFactory,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<SupplierController>();
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Supplier>>> Get()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            _logger.LogInformation("Log message in the GetAll() method");
            return _db.SupplierRepository.GetAll(userId).ToList();
        }

        [HttpGet("{id?}")]
        public async Task<ActionResult<Supplier>> Get(Supplier _supplier)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            Supplier supplier = _db.SupplierRepository.Get(userId, _supplier);
            if (supplier == null)
            {
                _logger.LogInformation($"INFO: {DateTime.Now} {GetType()} Not Found");
                return NotFound();
            }
            _logger.LogInformation($"INFO: {DateTime.Now} {GetType()} Success");
            return new ObjectResult(supplier);
        }

        [HttpPost("Post")]
        public async Task<ActionResult<Supplier>> Post(Supplier supplier)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            if (supplier.SupplierName == null)
            {
                return BadRequest();
            }
            //else if (_db.WareHouseRepository.GetByName(wareHouse.WareHouseName) != null)
            //{
            //    return BadRequest(); // Should change on "User already exist"
            //}
            await _db.SupplierRepository.Add(userId, supplier);
            return Ok(supplier);
        }

        [HttpPut("Put")]
        public async Task<ActionResult<Supplier>> Put(Supplier supplier)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            if (supplier == null)
            {
                return BadRequest();
            }
            if (_db.SupplierRepository.Get(userId, supplier) == null)
            {
                return NotFound();
            }

            await _db.SupplierRepository.Change(userId, supplier);
            return Ok(supplier);
        }

        [HttpDelete("{id?}")]
        public async Task<ActionResult<Supplier>> Delete(Supplier _supplier)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            Supplier supplier = _db.SupplierRepository.Get(userId, _supplier);
            if (supplier == null)
            {
                return NotFound();
            }
            await _db.SupplierRepository.Delete(userId, supplier);
            return Ok(supplier);
        }
    }
}