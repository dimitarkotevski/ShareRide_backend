using System.ComponentModel.DataAnnotations;

namespace ShareRide.API.Models;

public class Role
{
    [Key,Required] 
    public long Id { get; set; }
    public String Name { get; set; }
    public List<User> users { get; set; }
}