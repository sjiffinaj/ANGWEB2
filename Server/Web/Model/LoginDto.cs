using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
