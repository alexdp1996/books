using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AuthorVM
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public int CountOfBooks { get; set; }
        public virtual List<BookVM> Books { get; set; }

        public AuthorVM()
        {
            Books = new List<BookVM>();
        }
    }
}
