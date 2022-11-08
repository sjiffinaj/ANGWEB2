using System.ComponentModel.DataAnnotations;

namespace Web.Entity
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
