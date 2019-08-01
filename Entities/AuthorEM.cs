using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AuthorEM
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public virtual List<BookEM> Books { get; set; }
        
        public AuthorEM()
        {
            Books = new List<BookEM>();
        }
    }
}
