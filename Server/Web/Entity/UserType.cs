using System.ComponentModel.DataAnnotations;

namespace Web.Entity
{
    public class UserType : BaseEntity
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public long Value { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
