using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuarterlySales.Models;
using Microsoft.EntityFrameworkCore;

namespace QuarterlySales.Controllers
{
    public class EmployeeController : Controller
    {
        private QuarterlySalesContext context;

        public EmployeeController(QuarterlySalesContext ctx) => context = ctx;
        public IActionResult Index()
        {
            return View("Index", "Home");
        }

        [HttpGet]
        public ViewResult Add()
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
            //int temp = context.Employees.Count();
            //int[] employeeArray;
            //employeeArray = new int[temp];
            //for (int x = 0, y = 1; x < temp; x++, y++)
            //{
            //    vm.CurrentEmployee.EmployeeId = context.Employees.Find(y).EmployeeId;
            //    employeeArray[x] = vm.CurrentEmployee.EmployeeId;
            //}
            //ViewBag.EmployeeId = employeeArray;
            vm.Employees = context.Employees.ToList();
            //vm.Sales = context.Sales.ToList();
            return View(vm);//new QuarterlySalesViewModel()); //Employee());
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Add(QuarterlySalesViewModel employee)
        {
            QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
            vm.Employees = context.Employees.ToList();
            Employee checkFirstName = context.Employees.FirstOrDefault(e => e.FirstName == employee.CurrentEmployee.FirstName);
            Employee checkLastName = context.Employees.FirstOrDefault(e => e.LastName == employee.CurrentEmployee.LastName);
            Employee checkDateOfBirth = context.Employees.FirstOrDefault(e => e.DateOfBirth == employee.CurrentEmployee.DateOfBirth);
            string sameFirstName = vm.Employees.Find(e => e.EmployeeId == employee.CurrentEmployee.ManagerId).FirstName;
            string sameLastName = vm.Employees.Find(e => e.EmployeeId == employee.CurrentEmployee.ManagerId).LastName;
            DateTime? sameDOB = vm.Employees.Find(e => e.EmployeeId == employee.CurrentEmployee.ManagerId).DateOfBirth;

            if (checkFirstName != null && checkLastName != null && checkDateOfBirth != null)
            {
                ModelState.AddModelError("CurrentEmployee.DateOfBirth", $"{employee.CurrentEmployee.FirstName} {employee.CurrentEmployee.LastName} DOB({employee.CurrentEmployee.DateOfBirth}) is already in the database.");
            }

            if (sameFirstName != null && sameLastName != null && sameDOB != null)
            {
                ModelState.AddModelError("CurrentEmployee.ManagerId", $"Manager and employee can't be the same person.");
            }

            if (ModelState.IsValid)
            {
                employee.CurrentEmployee.EmployeeName = employee.CurrentEmployee.FirstName + " " + employee.CurrentEmployee.LastName;
                context.Employees.Add(employee.CurrentEmployee);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                vm.Employees = context.Employees.ToList();
                //vm.Sales = context.Sales.ToList();
                ModelState.AddModelError("", "Please correct all errors.");
                return View(vm);
            }
        }
    }
}
