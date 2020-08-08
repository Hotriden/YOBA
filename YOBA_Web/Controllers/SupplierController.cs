using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Supplier")]
    public class SupplierController : ControllerBase
    {
        private IUnitOfWork _db;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _userId;

        public SupplierController(
            IUnitOfWork db,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger<SupplierController>();
            _httpContextAccessor = httpContextAccessor;
            _userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Supplier>> GetAll()
        {
            var result = _db.SupplierRepository.GetAll(_userId).ToList();
            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                _logger.LogError($"{DateTime.Now} ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Supplier array is emtpy");
                return StatusCode(404, "Supplier array is empty");
            }
        }

        [HttpGet("Get/{id}")]
        public ActionResult<Supplier> Get(int id)
        {
            var result = _db.SupplierRepository.Get(_userId, new Supplier() { Id = id });
            if (result != null)
            {
                return result;
            }
            else
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Supplier {id} not found");
                return StatusCode(404, "Supplier not founded");
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Supplier>> Post(Supplier supplier)
        {
            if (supplier.SupplierName == null)
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Supplier {supplier.Id} not found");
                return BadRequest();
            }
            if (_userId != null)
            {
                supplier.UserId = _userId;
                await _db.SupplierRepository.Add(_userId, supplier);
            }
            return Ok(supplier);
        }


        [HttpPut("Put/{id}")]
        public async Task<ActionResult<Supplier>> Put(int id, [FromBody] Supplier supplierBody)
        {

            if (supplierBody == null)
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Supplier {id} not found");
                return BadRequest("Supplier is empty");
            }
            if (id != supplierBody.Id)
            {
                return BadRequest();
            }
            Supplier supplier = _db.SupplierRepository.Get(_userId, new Supplier() { Id = id });
            if (supplier == null)
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Supplier {id} not found");
                return BadRequest("Supplier doesn't exist");
            }
            else
            {
                await _db.SupplierRepository.Change(_userId, supplierBody);
                return Ok(supplier);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Supplier>> Delete(int id)
        {
            Supplier supplier = _db.SupplierRepository.Get(_userId, new Supplier() { Id = id });
            if (supplier == null)
            {
                _logger.LogError($"{DateTime.Now} ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Supplier {id} not found");
                return NotFound();
            }
            else
            {
                await _db.SupplierRepository.Delete(_userId, supplier);
                return Ok(supplier);
            }
        }
    }
}