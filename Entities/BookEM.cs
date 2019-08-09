using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BookEM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public int Pages { get; set; }
        public List<AuthorEM> Authors { get; set; }
        public DateTime Date { get; set; }

        public BookEM()
        {
            Authors = new List<AuthorEM>();
        }
    }
}
