namespace ShareRide.API.Exception;

public class UserIdNotExistException : System.Exception
{
    public UserIdNotExistException(string massage) : base(massage)
    {
    }
}