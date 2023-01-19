namespace ShareRide.API.Dto;

public class RoleDto
{
    public string Role{get;set;}

    public RoleDto(string role)
    {
        Role = role;
    }
}