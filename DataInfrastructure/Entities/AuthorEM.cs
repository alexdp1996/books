using System.Collections.Generic;

namespace DataInfrastructure.Entities
{
    public class AuthorEM : BaseEM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual List<BookEM> Books { get; set; }
        
        public AuthorEM()
        {
            Books = new List<BookEM>();
        }
    }
}
