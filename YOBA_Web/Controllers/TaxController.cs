using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : Controller
    {
        private IUnitOfWork db;

        public TaxController(IUnitOfWork context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<List<Tax>> Get()
        {
            return db.TaxRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Tax> Get(int id)
        {
            Tax tax = db.TaxRepository.GetById(id);
            if (tax == null)
                return NotFound();
            return new ObjectResult(tax);
        }

        [HttpPost]
        public async Task<ActionResult<Tax>> Post(Tax tax)
        {
            if (tax == null)
            {
                return BadRequest();
            }

            await db.TaxRepository.Add(tax);
            return Ok(tax);
        }

        [HttpPut]
        public ActionResult<Tax> Put(Tax tax)
        {
            if (tax == null)
            {
                return BadRequest();
            }
            if (db.TaxRepository.GetById(tax.Id) == null)
            {
                return NotFound();
            }

            db.TaxRepository.Change(tax);
            return Ok(tax);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public ActionResult<Tax> Delete(int id)
        {
            Tax tax = db.TaxRepository.GetById(id);
            if (tax == null)
            {
                return NotFound();
            }
            db.TaxRepository.Delete(tax);
            return Ok(tax);
        }
        }
}