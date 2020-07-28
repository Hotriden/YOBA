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
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Tax")]
    public class TaxController : ControllerBase
    {
        private IUnitOfWork _db;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _userId;

        public TaxController(
            IUnitOfWork db,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger<TaxController>();
            _httpContextAccessor = httpContextAccessor;
            _userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Tax>> GetAll()
        {
            var result = _db.TaxRepository.GetAll(_userId).ToList();
            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                _logger.LogError($"{DateTime.Now} ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Tax array is emtpy");
                return StatusCode(404, "Tax array is empty");
            }
        }

        [HttpGet("Get/{id}")]
        public ActionResult<Tax> Get(int id)
        {
            var result = _db.TaxRepository.Get(_userId, new Tax() { Id = id });
            if (result != null)
            {
                return result;
            }
            else
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Tax {id} not found");
                return StatusCode(404, "Tax not founded");
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Tax>> Post(Tax tax)
        {
            if (tax.Name == null)
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Tax {tax.Id} not found");
                return BadRequest();
            }
            if (_userId != null)
            {
                tax.UserId = _userId;
                await _db.TaxRepository.Add(_userId, tax);
            }
            return Ok(tax);
        }


        [HttpPut("Put/{id}")]
        public async Task<ActionResult<Tax>> Put(int id, [FromBody] Tax taxBody)
        {

            if (taxBody == null)
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Tax {id} not found");
                return BadRequest("Tax is empty");
            }
            if (id != taxBody.Id)
            {
                return BadRequest();
            }
            Tax tax = _db.TaxRepository.Get(_userId, new Tax() { Id = id });
            if (tax == null)
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Tax {id} not found");
                return BadRequest("Tax doesn't exist");
            }
            else
            {
                await _db.TaxRepository.Change(_userId, taxBody);
                return Ok(tax);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Tax>> Delete(int id)
        {
            Tax tax = _db.TaxRepository.Get(_userId, new Tax() { Id = id });
            if (tax == null)
            {
                _logger.LogError($"{DateTime.Now} ERROR. UserId: {_userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Tax {id} not found");
                return NotFound();
            }
            else
            {
                await _db.TaxRepository.Delete(_userId, tax);
                return Ok(tax);
            }
        }
    }
}