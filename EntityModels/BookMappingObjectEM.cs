using System.Collections.Generic;

namespace EntityModels
{
    public class BookMappingObjectEM
    {
        public BookEM Book { get; set; }
        public List<AuthorEM> Authors { get; set; }
    }
}
