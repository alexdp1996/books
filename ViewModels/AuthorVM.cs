using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class AuthorVM : AuthorBaseVM
    {
        [Display(Name = "Amount of books")]
        public int CountOfBooks { get; set; }
        public List<BookBaseVM> Books { get; set; }

        public AuthorVM()
        {
            Books = new List<BookBaseVM>();
        }
    }
}
