using System.Collections.Generic;

namespace ViewModels
{
    public class BookEditVM: BookVM
    {
        public List<long> AuthorIds { get; set; }

        public BookEditVM()
        {
            AuthorIds = new List<long>();
        }
    }
}
