using System.ComponentModel.DataAnnotations;

namespace Web.Entity
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public long CreatedUser { get; set; }
        public long ModifiedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
