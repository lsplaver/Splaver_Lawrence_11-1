using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QuarterlySales.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a date of birth.")]
        [PastDate(ErrorMessage = "Please enter a date of birth in the past.")]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter a hire date.")]
        // [Range(typeof(DateTime), "1/1/1995", "11/9/2021", ErrorMessage = "Please enter a date between 1/1/1995 and 11/9/2021.")]
        [YearsFromNow(26, IsPast = true)]
        [Display(Name = "Hire Date")]
        public DateTime? DateOfHire { get; set; }

        [Required(ErrorMessage = "Please enter a manager.")]
        [Display(Name = "Manager")]
        public int ManagerId { get; set; }

        public string EmployeeName { get; set; }

        public Employee()
        {
            EmployeeName = FirstName + " " + LastName;
        }
    }
}
