using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Entity
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }

        public string  PhotoUrl { get; set; }

        public byte Sex { get; set; }

        public int UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
