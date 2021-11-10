using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QuarterlySales.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        [Required(ErrorMessage = "Please enter a sales quarter.")]
        [Range(1, 4, ErrorMessage = "Please enter a quarter between 1 and 4.")]
        public int Quarter { get; set; }

        [Required(ErrorMessage = "Please enter a sales year.")]
        [Range(2000, Int32.MaxValue, ErrorMessage = "Please enter a year after 2000.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter a sales amountl")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a sales amount greater than 0.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Please select an employee.")]
        public int EmployeeId { get; set; } 
        public Employee Employee { get; set; }
    }
}
