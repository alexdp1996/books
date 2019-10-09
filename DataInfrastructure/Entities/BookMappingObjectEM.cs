using System.Collections.Generic;

namespace DataInfrastructure.Entities
{
    public class BookMappingObjectEM
    {
        public BookEM Book { get; set; }
        public List<AuthorEM> Authors { get; set; }
    }
}
