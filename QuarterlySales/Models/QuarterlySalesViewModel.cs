using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Models
{
    public class QuarterlySalesViewModel
    {
        public List<Sale> Sales { get; set; }
        public List<Employee> Employees { get; set; }
        public string Name { get; set; }
        public double TotalSales { get; set; }

        //public int id { get; }

        //public string GetName(int id)
        //{
        //    QuarterlySalesViewModel vm = new QuarterlySalesViewModel();
        //    vm.Name = vm.Employees.Find(id).FirstName.ToString() + " " + vm.Employees.Find(id).LastName.ToString();
        //    return Name;
        //}
    }
}
