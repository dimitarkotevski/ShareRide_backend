namespace ShareRide.API.Models;

public class ProfilePicture
{
    public int Id { get; set; }
    public String Name { get; set; }
    public String MimeType { get; set; }
    public byte[] PhotoByteArray { get; set; }
}