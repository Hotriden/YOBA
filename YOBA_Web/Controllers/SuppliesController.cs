using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YOBA_BLL.Catalogue;
using YOBA_BLL.Supply;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_Web.Models;

namespace YOBA_Web.Controllers
{
    [Route("api/[controller]")]
    public class SuppliesController : Controller
    {
        static readonly List<SupplierD> data; //Fake

        static SuppliesController()
        {
            data = new List<SupplierD>
            {
                new SupplierD { Id = Guid.NewGuid().ToString(), Name="iPhone 7" },
                new SupplierD { Id = Guid.NewGuid().ToString(), Name="Samsung Galaxy S7"},
            };
        }

        [HttpGet]
        public IEnumerable<SupplierD> Get()
        {
            return data; // FAKE
            //return catalogue.SupplyCatalogue.SupplierCatalogue.GetAll();
        }

        [HttpPost]
        public IActionResult Post(SupplierD supplier)
        {
            supplier.Id = Guid.NewGuid().ToString();
            data.Add(supplier);
            return Ok(supplier);
        }

        // FAKE
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            SupplierD supplier = data.FirstOrDefault(x => x.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }
            data.Remove(supplier);
            return Ok(supplier);
        }
    }
}