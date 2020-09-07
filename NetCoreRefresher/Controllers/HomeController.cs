using System;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreRefresher.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Home");
        }

        public IActionResult ErrorSimulator()
        {
            throw new Exception("Simulated error");
        }
    }
}