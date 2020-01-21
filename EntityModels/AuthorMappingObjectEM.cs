using System.Collections.Generic;

namespace EntityModels
{
    public class AuthorMappingObjectEM
    {
        public AuthorEM Author { get; set; }
        public List<BookEM> Books { get; set; }
    }
}
