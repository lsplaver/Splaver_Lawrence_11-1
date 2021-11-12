using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuarterlySales.Models;
using Microsoft.EntityFrameworkCore;

namespace QuarterlySales.Controllers
{
    public class SalesController : Controller
    {
        private QuarterlySalesContext context;

        public SalesController(QuarterlySalesContext ctx) => context = ctx;
        public IActionResult Index()
        {
            return View("Index", "Home");
        }

        [HttpGet]
        public ViewResult Add()
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
            vm.Sales = context.Sales.ToList();
            vm.Employees = context.Employees.ToList();
            return View(vm);
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Add(QuarterlySalesViewModel sale)
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
            Sale checkQuarter = context.Sales.FirstOrDefault(s => s.Quarter == sale.CurrentSale.Quarter);
            Sale checkYear = context.Sales.FirstOrDefault(s => s.Year == sale.CurrentSale.Year);
            Sale checkEmployee = context.Sales.FirstOrDefault(s => s.EmployeeId == sale.CurrentSale.EmployeeId);

            if (checkQuarter != null && checkYear != null && checkEmployee != null)
            {
                ModelState.AddModelError("EmployeeId", $"Sales for {sale.CurrentSale.EmployeeId} for {sale.CurrentSale.Year} {sale.CurrentSale.Quarter} are already in the database");
            }

            if (ModelState.IsValid)
            {
                context.Sales.Add(sale.CurrentSale);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                vm.Sales = context.Sales.ToList();
                vm.Employees = context.Employees.ToList();
                ModelState.AddModelError("", "Please correct all errors");
                return View(vm);
            }
        }
    }
}
