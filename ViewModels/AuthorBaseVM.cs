using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class AuthorBaseVM
    {
        public long? Id { get; set; }
        [Required]
        [MinLength(4)]
        [RegularExpression(@"[A-Za-z]*", ErrorMessage = "Only letters are allowed")]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        [RegularExpression(@"[A-Za-z]*", ErrorMessage = "Only letters are allowed")]
        public string Surname { get; set; }
        [Display(Name = "Amount of books")]
        public int CountOfBooks { get; set; }
    }
}
