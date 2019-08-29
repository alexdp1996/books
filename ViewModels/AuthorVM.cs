using System.Collections.Generic;

namespace ViewModels
{
    public class AuthorVM : AuthorBaseVM
    {       
        public List<BookBaseVM> Books { get; set; }

        public AuthorVM()
        {
            Books = new List<BookBaseVM>();
        }
    }
}
