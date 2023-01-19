using System.ComponentModel.DataAnnotations;

namespace ShareRide.API.Models
{
    public class VerificationCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpire { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
