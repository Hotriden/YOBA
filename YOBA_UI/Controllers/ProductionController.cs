﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YOBA_UI.Controllers
{
    public class ProductionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}