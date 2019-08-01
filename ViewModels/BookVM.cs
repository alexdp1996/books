using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BookVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public List<AuthorVM> Authors { get; set; }

        public BookVM()
        {
            Authors = new List<AuthorVM>();
        }
    }
}
