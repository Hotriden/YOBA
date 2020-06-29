using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using YOBA_LibraryData.BLL.Interfaces;
using Microsoft.Extensions.Logging;
using YOBA_LibraryData.BLL.Entities.Supply;

namespace YOBA_Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Supplier")]
    public class SupplierController : ControllerBase
    {
        private IUnitOfWork _db;
        private readonly ILogger _logger;

        public SupplierController(IUnitOfWork db, ILoggerFactory loggerFactory)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger<WarehouseController>();
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Supplier>> Get()
        {
            _logger.LogInformation("Log message in the GetAll() method");
            return _db.SupplierRepository.GetAll().ToList();
        }

        [HttpGet("{id?}")]
        public ActionResult<Supplier> Get(int id)
        {
            Supplier supplier = _db.SupplierRepository.GetById(id);
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
            if (supplier.SupplierName == null)
            {
                return BadRequest();
            }
            //else if (_db.WareHouseRepository.GetByName(wareHouse.WareHouseName) != null)
            //{
            //    return BadRequest(); // Should change on "User already exist"
            //}
            await _db.SupplierRepository.Add(supplier);
            return Ok(supplier);
        }

        [HttpPut("Put")]
        public ActionResult<Supplier> Put(Supplier supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }
            if (_db.SupplierRepository.GetById(supplier.SupplierId) == null)
            {
                return NotFound();
            }

            _db.SupplierRepository.Change(supplier);
            return Ok(supplier);
        }

        [HttpDelete("{id?}")]
        public ActionResult<Supplier> Delete(int id)
        {
            Supplier supplier = _db.SupplierRepository.GetById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            _db.SupplierRepository.Delete(supplier);
            return Ok(supplier);
        }
    }
}