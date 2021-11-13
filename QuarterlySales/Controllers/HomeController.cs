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
        //private QuarterlySalesContext context { get; set; }
        private QuarterlySalesContext context;

        //public HomeController(QuarterlySalesContext context) => this.context = context;
        public HomeController(QuarterlySalesContext ctx) => context = ctx;
        [HttpGet]
        [Route("{controller}/{action}/{id?}")]
        [Route("/")]
        public IActionResult Index(string id)
        {
            //IQueryable<QuarterlySalesViewModel> sales = context.Sales
            //    .Include(s => s.Employee)
            //    .OrderBy(s => s.SaleId);

            //var sales = context.Sales.Include(s => s.Employee).OrderBy(s => s.SaleId);

            //if (id != 0)
            //{
            //    sales = (IOrderedQueryable<Sale>)sales.Where(s => s.Employee.EmployeeId == id);
            //}

            //QuarterlySalesViewModel model = new QuarterlySalesViewModel
            //{
            //    Sale = (Sale)sls
            //};

            //Sale sales = context.Sales.Include(s => s.Employee).OrderBy(s => s.SaleId).ToList();

            //var filters = new Filt

            QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
            vm.Employees = context.Employees.ToList();
            vm.Sales = context.Sales.ToList();

            IQueryable<Sale> query = context.Sales.Include(s => s.Employee);

            if (id != null)
            {
                query = query.Where(s => s.EmployeeId == int.Parse(id));
            }

            var sales = query.OrderBy(s => s.SaleId).ToList();

            vm.Sales = sales;
            double totalSales = 0;
            for (int y = 1; y <= context.Sales.Count(); y++)
            {
                totalSales += context.Sales.Find(y).Amount;
            }
            vm.TotalSales = totalSales;
            return View(vm);
        }

        //[HttpPost]
        //public RedirectToActionResult Index(int id)
        //{

        //}

        public string GetName(int id)
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
            vm.Name = context.Employees.Find(id).FirstName;
            vm.Name = vm.Name + " ";
            vm.Name = vm.Name + context.Employees.Find(id).LastName;
            return vm.Name;
        }

        public double GetTotalSales()
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
            double totalSales = 0;
            vm.Sales = context.Sales.ToList();
            for (int x = 0; x < context.Sales.Count(); x++)
            {
                totalSales += context.Sales.Find(x).Amount;
            }
            vm.TotalSales = totalSales;
            return vm.TotalSales;
        }

        [HttpPost]
        public RedirectToActionResult Filter(int EmpId)
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
            vm.Empid = EmpId;
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
