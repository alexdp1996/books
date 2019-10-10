using System;
using System.Collections.Generic;

namespace DataInfrastructure.Entities
{
    public class BookEM : BaseEM
    {
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
