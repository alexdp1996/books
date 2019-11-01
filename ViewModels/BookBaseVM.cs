using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class BookBaseVM
    {
        public long? Id { get; set; }
        [Required]
        [MinLength(4)]
        [RegularExpression(@"[^\s]+(.*[^\s]+)*", ErrorMessage = "String must be trimmed")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Value beetween 1 and 10 is required")]
        [Range(1, 10, ErrorMessage = "Value beetween 1 and 10 is required")]
        public int Rate { get; set; }
        [Required(ErrorMessage = "Value beetween 25 and 10000 is required")]
        [Range(25, 10000, ErrorMessage = "Value beetween 25 and 10000 is required")]
        public int Pages { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
