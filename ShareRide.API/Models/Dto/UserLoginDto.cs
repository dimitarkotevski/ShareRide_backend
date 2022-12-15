using System.ComponentModel.DataAnnotations;

namespace ShareRide.API.Models.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
