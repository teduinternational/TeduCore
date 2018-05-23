﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeduCore.WebAdmin.Models;

namespace TeduCore.WebAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}