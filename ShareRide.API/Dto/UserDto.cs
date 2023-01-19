namespace ShareRide.API.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime DateCreated { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string Role { get; set; }
}