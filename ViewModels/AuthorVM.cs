using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class AuthorVM
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Display(Name = "Amount of books")]
        public int CountOfBooks { get; set; }
        public virtual List<BookVM> Books { get; set; }

        public AuthorVM()
        {
            Books = new List<BookVM>();
        }
    }
}
