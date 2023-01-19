using System.ComponentModel.DataAnnotations;

namespace ShareRide.API.Dto.RegisterAndLoginDto
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public int RoleId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
