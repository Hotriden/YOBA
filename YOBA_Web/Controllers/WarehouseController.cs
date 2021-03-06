﻿using System;
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
    [Route("api/WareHouse")]
    public class WarehouseController : ControllerBase
    {
        private IUnitOfWork _db;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string userId;

        public WarehouseController(
            IUnitOfWork db, 
            ILoggerFactory loggerFactory, 
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger<WarehouseController>();
            _httpContextAccessor = httpContextAccessor;
            userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<WareHouse>> GetAll()
        {
            _logger.LogInformation("Log message in the GetAll() method");
            var result = _db.WareHouseRepository.GetAll(userId).ToList();
            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                _logger.LogError($"{DateTime.Now} ERROR. UserId: {userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Warehouses array is emtpy");
                return StatusCode(404, "WareHouses array is empty");
            }
        }

        [HttpGet("Get/{id}")]
        public ActionResult<WareHouse> Get(int id)
        {
            var result = _db.WareHouseRepository.Get(userId, new WareHouse() { Id = id });
            if (result != null)
            {
                _logger.LogInformation($"UserId: {userId}, INFO: {DateTime.Now} {GetType()} Success");
                return result;
            }
            else
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Warehouse {id} not found");
                return StatusCode(404, "WareHouse not founded");
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<WareHouse>> Post(WareHouse wareHouse)
        {
            if (wareHouse.WareHouseName == null)
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Warehouse {wareHouse.Id} not found");
                return BadRequest();
            }
            if (userId != null)
            {
                wareHouse.UserId = userId;
                await _db.WareHouseRepository.Add(userId, wareHouse);
            }
            return Ok(wareHouse);
        }


        [HttpPut("Put/{id}")]
        public async Task<ActionResult<WareHouse>> Put(int id, [FromBody] WareHouse wh)
        {
            if (wh == null)
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Warehouse {id} not found");
                return BadRequest("Warehouse is empty");
            }
            if (id != wh.Id)
            {
                return BadRequest();
            }
            WareHouse wareHouse = _db.WareHouseRepository.Get(userId, new WareHouse() { Id = id });
            if (wareHouse == null)
            {
                _logger.LogError($"{DateTime.Now} - ERROR. UserId: {userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Warehouse {id} not found");
                return BadRequest("Warehouse doesn't exist");
            }
            else
            {
                await _db.WareHouseRepository.Change(userId, wh);
                return Ok(wareHouse);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<WareHouse>> Delete(int id)
        {
            WareHouse wareHouse = _db.WareHouseRepository.Get(userId, new WareHouse() { Id = id });
            if (wareHouse == null)
            {
                _logger.LogError($"{DateTime.Now} ERROR. UserId: {userId}. \nController: {GetType().Name} " +
                    $"\nMethod: {new StackTrace().GetFrame(0).GetMethod()} \nErrorMessage: Warehouse {id} not found");
                return NotFound();
            }
            else
            {
                await _db.WareHouseRepository.Delete(userId, wareHouse);
                return Ok(wareHouse);
            }
        }
    }
}