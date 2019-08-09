using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.DataAttributes
{
    public class DateRange : ValidationAttribute
    {
        public DateRange()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            return dt <= DateTime.Now;
        }
    }
}
