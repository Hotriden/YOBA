using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private IUnitOfWork _db;
        private readonly ILogger _logger;

        public WarehouseController(IUnitOfWork db, ILoggerFactory loggerFactory)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger<WarehouseController>();
        }

        [HttpGet]
        public ActionResult<List<WareHouse>> Get()
        {
            _logger.LogInformation("Log message in the GetAll() method");
            return _db.WareHouseRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<WareHouse> Get(int id)
        {
            WareHouse wareHouse = _db.WareHouseRepository.GetById(id);
            if (wareHouse == null)
            {
                _logger.LogInformation($"INFO: {DateTime.Now} {this.GetType()} Not Found");
                return NotFound();
            }
            _logger.LogInformation($"INFO: {DateTime.Now} {this.GetType()} Success");
            return new ObjectResult(wareHouse);
        }

        [HttpPost]
        public async Task<ActionResult<WareHouse>> Post(WareHouse wareHouse)
        {
            if (wareHouse.WareHouseName == null)
            {
                return BadRequest();
            }
            //else if (_db.WareHouseRepository.GetByName(wareHouse.WareHouseName) != null)
            //{
            //    return BadRequest(); // Should change on "User already exist"
            //}
            await _db.WareHouseRepository.Add(wareHouse);
            return Ok(wareHouse);
        }

        [HttpPut]
        public ActionResult<WareHouse> Put(WareHouse wareHouse)
        {
            if (wareHouse == null)
            {
                return BadRequest();
            }
            if (_db.WareHouseRepository.GetById(wareHouse.Id)==null)
            {
                return NotFound();
            }

            _db.WareHouseRepository.Change(wareHouse);
            return Ok(wareHouse);
        }

        [HttpDelete("{id}")]
        public ActionResult<WareHouse> Delete(int id)
        {
            WareHouse wareHouse = _db.WareHouseRepository.GetById(id);
            if (wareHouse == null)
            {
                return NotFound();
            }
            _db.WareHouseRepository.Delete(wareHouse);
            return Ok(wareHouse);
        }
    }
}