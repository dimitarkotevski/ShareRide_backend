using System.ComponentModel.DataAnnotations;

namespace ShareRide.API.Dto.RegisterAndLoginDto
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
