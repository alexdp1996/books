using System.Collections.Generic;

namespace ViewModels
{
    public class BookVM : BookBaseVM
    {
        public List<AuthorBaseVM> Authors { get; set; }

        public BookVM()
        {
            Authors = new List<AuthorBaseVM>();
        }
    }
}
