using System.Collections.Generic;

namespace DataInfrastructure.Entities
{
    public class AuthorMappingObjectEM
    {
        public AuthorEM Author { get; set; }
        public List<BookEM> Books { get; set; }
    }
}
