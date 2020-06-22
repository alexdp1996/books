using System.Collections.Generic;

namespace ViewModels
{
    public class AuthorVM
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? CountOfBooks { get; set; }
        public List<BookVM> Books { get; set; }

        public AuthorVM()
        {
            Books = new List<BookVM>();
        }
    }
}
