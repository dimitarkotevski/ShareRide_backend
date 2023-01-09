using ShareRide.API.Models.Dto;
using ShareRide.API.Security.Hashing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public DateTime? dateCreated { get; set; }

        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public User(string username, string email, string password, DateTime? dateCreated, string? address, string? phoneNumber, int roleId, Role role)
        {
            Username = username;
            Email = email;
            Password = password;
            this.dateCreated = dateCreated;
            Address = address;
            PhoneNumber = phoneNumber;
            RoleId = roleId;
            Role = role;
        }

        public User()
        {
        }
    }
}
