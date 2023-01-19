namespace ShareRide.API.Exception;

public class UserNotFoundException : System.Exception
{
    public UserNotFoundException(string massage): base(massage) {}
}