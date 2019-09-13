using System.Collections.Generic;

namespace ViewModels
{
    public class BookEditVM: BookBaseVM
    {
        public List<long> AuthorIds { get; set; }

        public BookEditVM()
        {
            AuthorIds = new List<long>();
        }
    }
}
