using System;
using System.ComponentModel.DataAnnotations;
using ViewModels.DataAttributes;

namespace ViewModels
{
    public class BookBaseVM
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
        public DateTime Date { get; set; }
    }
}
