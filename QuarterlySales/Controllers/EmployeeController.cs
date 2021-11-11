using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuarterlySales.Models;

namespace QuarterlySales.Controllers
{
    public class EmployeeController : Controller
    {

        public IActionResult Index()
        {
            return View("Index", "Home");
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View(new Employee());
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public IActionResult Add(Employee employee)
        //{

        //}
    }
}
