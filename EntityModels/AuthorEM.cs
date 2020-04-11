using System.Collections.Generic;

namespace EntityModels
{
    public class AuthorEM
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual List<BookEM> Books { get; set; }
        
        public AuthorEM()
        {
            Books = new List<BookEM>();
        }
    }
}
