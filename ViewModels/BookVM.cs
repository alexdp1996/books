using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class BookVM
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public int Pages { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<AuthorVM> Authors { get; set; }
        
        public BookVM()
        {
            Authors = new List<AuthorVM>();
        }
    }
}
