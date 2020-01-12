using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YOBA_BLL.Catalogue;
using YOBA_LibraryData.BLL.Entities.Supply;

namespace YOBA_Web.Controllers
{
    public class SupplyController : Controller
    {
        private ICatalogue<Supplier> supplyCatalogue;

        public SupplyController(ICatalogue<Supplier> catalogue)
        {
            supplyCatalogue = catalogue;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateSupplier(Supplier supplier, IdentityUser user)
        {
            supplyCatalogue.Create(supplier, user.Id);
            return View();
        }
    }
}