using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class AuthorBaseVM
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}
