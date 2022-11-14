using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class UserTypeDto
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
