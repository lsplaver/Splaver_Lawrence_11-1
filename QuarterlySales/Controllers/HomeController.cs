using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuarterlySales.Models;
using Microsoft.EntityFrameworkCore;

namespace QuarterlySales.Controllers
{
    public class HomeController : Controller
    {
        private QuarterlySalesContext context;

        public HomeController(QuarterlySalesContext ctx) => context = ctx;

        public IActionResult Index(string id)
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel { Employees = context.Employees.ToList(), Sales = context.Sales.ToList() };

            IQueryable<Sale> query = context.Sales.Include(s => s.Employee);

            int tempId = 0;

            if (id != null)
            {
                tempId = int.Parse(id);
            }

            if (tempId > 0)
            {
                query = query.Where(s => s.EmployeeId == tempId);
            }

            var sales = query.OrderBy(s => s.SaleId).ToList();

            vm.Sales = sales;
            double totalSales = 0;

            if (tempId == 0)
            {
                for (int y = 1; y <= context.Sales.Count(); y++)
                {
                    totalSales += context.Sales.Find(y).Amount;
                }
            }
            else
            {
                foreach (var temp in vm.Sales)
                {
                    totalSales += vm.Sales.Find(s => s.EmployeeId == tempId).Amount;
                }
            }
            vm.TotalSales = totalSales;
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Filter(int EmpId)
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel { Empid = EmpId };
            if (vm.Empid > 0)
            {
                return RedirectToAction("Index", new { id = vm.Empid.ToString() });
            }
            else
            {
                return RedirectToAction("Index", new { id = string.Empty });
            }
        }
    }
}
