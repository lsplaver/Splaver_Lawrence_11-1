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
        public double TotalSales { get; set; }
        public Employee CurrentEmployee { get; set; }
        public Sale CurrentSale { get; set; }
        public int Empid { get; set; }
    }
}
