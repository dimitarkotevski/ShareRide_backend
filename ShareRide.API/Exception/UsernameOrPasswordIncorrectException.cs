namespace ShareRide.API.Exception;

public class UsernameOrPasswordIncorrectException : System.Exception
{
    public UsernameOrPasswordIncorrectException(string massage): base(massage) { }
}