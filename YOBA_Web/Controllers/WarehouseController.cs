﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger _logger;

        public WarehouseController(IUnitOfWork db, ILoggerFactory loggerFactory)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger<WarehouseController>();
        }

        [HttpGet("GetAll")]
        public ActionResult<List<WareHouse>> GetAll(IdentityUser user)
        {
            _logger.LogInformation("Log message in the GetAll() method");
            var result = _db.WareHouseRepository.GetAll().ToList();
            return result;
        }

        [HttpGet("{id?}")]
        public ActionResult<WareHouse> Get(IdentityUser user, WareHouse wareHouse)
        {
            WareHouse _wareHouse = _db. .WareHouseRepository.GetById(user.Id);
            if (wareHouse == null)
            {
                _logger.LogInformation($"INFO: {DateTime.Now} {GetType()} Not Found");
                return NotFound();
            }
            _logger.LogInformation($"INFO: {DateTime.Now} {GetType()} Success");
            return new ObjectResult(wareHouse);
        }

        [HttpPost("Post")]
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

        [HttpPut("Put")]
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

        [HttpDelete("{id?}")]
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