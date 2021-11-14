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
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel { Sales = context.Sales.ToList(), Employees = context.Employees.ToList() };
            return View(vm);
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Add(QuarterlySalesViewModel sale)
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel { Sales = context.Sales.ToList(), Employees = context.Employees.ToList() };
            Sale checkQuarter = context.Sales.FirstOrDefault(s => s.Quarter == sale.CurrentSale.Quarter);
            Sale checkYear = context.Sales.FirstOrDefault(s => s.Year == sale.CurrentSale.Year);
            Sale checkEmployee = context.Sales.FirstOrDefault(s => s.EmployeeId == sale.CurrentSale.EmployeeId);
            string fullName = context.Employees.Find(sale.CurrentSale.EmployeeId).FullName;

            if (checkQuarter != null && checkYear != null && checkEmployee != null)
            {
                ModelState.AddModelError("CurrentSale.EmployeeId", $"Sales for {fullName} for {sale.CurrentSale.Year} Q{sale.CurrentSale.Quarter} are already in the database");
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
