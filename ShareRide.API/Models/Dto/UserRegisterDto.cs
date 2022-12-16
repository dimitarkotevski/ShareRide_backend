using System.ComponentModel.DataAnnotations;

namespace ShareRide.API.Models.Dto
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }

        public Role Role { get; set; }
    }
}
