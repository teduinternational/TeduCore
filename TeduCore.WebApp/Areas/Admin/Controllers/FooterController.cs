﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeduCore.WebApp.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}