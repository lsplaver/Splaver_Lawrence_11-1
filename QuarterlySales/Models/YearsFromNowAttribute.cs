using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QuarterlySales.Models
{
    public class YearsFromNowAttribute : ValidationAttribute
    {
        private int numYears;
        public YearsFromNowAttribute(int years)
        {
            numYears = years;
        }
        public bool IsPast { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            if (value is DateTime)
            {
                DateTime dateToCheck = (DateTime)value;

                DateTime now = DateTime.Today;
                DateTime from;

                if (IsPast)
                {
                    from = new DateTime(now.Year, 1, 1);
                    from = from.AddYears(-numYears);
                }
                else
                {
                    from = new DateTime(now.Year, 12, 31);
                    from = from.AddYears(numYears);
                }

                if (IsPast)
                {
                    if (dateToCheck >= from && dateToCheck < now)
                    {
                        return ValidationResult.Success;
                    }
                }
                else
                {
                    if (dateToCheck > now && dateToCheck <= from)
                    {
                        return ValidationResult.Success;
                    }
                }

            }

            string msg = base.ErrorMessage ?? ctx.DisplayName + " must be a " + (IsPast ? "past" : "future") + " date within " + numYears + " years of now.";
            return new ValidationResult(msg);
        }
    }
}
