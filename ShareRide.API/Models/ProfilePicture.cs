using System.ComponentModel.DataAnnotations;

namespace ShareRide.API.Models;
public class ProfilePicture
{
    [Key]
    public int Id { get; set; }
    [Required]
    public String Name { get; set; }
    [Required]
    public String MimeType { get; set; }
    [Required]
    public byte[] PhotoByteArray { get; set; }
    public virtual User User { get; set; }
}