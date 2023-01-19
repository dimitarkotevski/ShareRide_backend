using System.ComponentModel.DataAnnotations;

namespace ShareRide.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsEnabled { get; set; }

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int? ProfilePictureId { get; set; }
        public virtual ProfilePicture? ProfilePicture { get; set; }
        public int? VerificationCodeId { get; set; }
        public virtual VerificationCode? VerificationCode { get; set; }
        public User()
        {
        }
    }
}
