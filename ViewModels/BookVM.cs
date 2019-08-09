using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DataAttributes;

namespace ViewModels
{
    public class BookVM
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required(ErrorMessage = "Value beetween 1 and 10 is required")]
        [Range(1, 10, ErrorMessage = "Value beetween 1 and 10 is required")]
        public int Rate { get; set; }
        [Required(ErrorMessage = "Value beetween 25 and 10000 is required")]
        [Range(25, 10000, ErrorMessage = "Value beetween 25 and 10000 is required")]
        public int Pages { get; set; }
        [Required]
        [DateRange(ErrorMessage = "Invalid date")]
        public DateTime Date{ get; set; }
        public List<AuthorVM> Authors { get; set; }
        public List<long> AuthorIds { get; set; }

        public BookVM()
        {
            Authors = new List<AuthorVM>();
            AuthorIds = new List<long>();
        }
    }
}
