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

        //[HttpGet]
        //[Route("{controller}/{action}/{id?}")]
        //[Route("/")]
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

            QuarterlySalesViewModel vm = new QuarterlySalesViewModel { Employees = context.Employees.ToList(), Sales = context.Sales.ToList() };

            IQueryable<Sale> query = context.Sales.Include(s => s.Employee);

            int tempId = 0;

            if (id != null)
            {
                tempId = int.Parse(id);
            }
            //else
            //{
            //    tempId = 0;
            //}

            if (tempId > 0)
            {
                query = query.Where(s => s.EmployeeId == tempId);
            }

            var sales = query.OrderBy(s => s.SaleId).ToList();

            vm.Sales = sales;
            double totalSales = 0;
            //for (int y = 1; y <= vm.Sales.Count() /*context.Sales.Count()*/; y++)
            //{
            //}.Find(y).Amount;
            //}
            //for (int y = 1; y <= vm.Sales.Count(); y++)
            //{
            //    if (tempId != 0)
            //    {
            //        totalSales += vm.Sales.Find(s => s.EmployeeId == tempId).Amount;
            //    }
            //    else
            //    {
            //        totalSales += vm.Sales.Find(s => s.SaleId == y).Amount;
            //    }
            //}
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
            //Employee employee e(tempId);
            return View(vm);
        }

        //[HttpPost]
        //public RedirectToActionResult Index(int id)
        //{

        //}

        //public string GetName(int id)
        //{
        //    QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
        //    vm.Name = context.Employees.Find(id).FirstName;
        //    vm.Name = vm.Name + " ";
        //    vm.Name = vm.Name + context.Employees.Find(id).LastName;
        //    return vm.Name;
        //}

        //public double GetTotalSales()
        //{
        //    QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
        //    double totalSales = 0;
        //    for (int x = 0; x < context.Sales.Count(); x++)
        //    {
        //        totalSales += context.Sales.Find(x).Amount;
        //    }
        //    vm.TotalSales = totalSales;
        //    return vm.TotalSales;
        //}

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
