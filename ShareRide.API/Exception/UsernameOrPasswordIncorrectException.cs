namespace ShareRide.API.Exception;

public class UsernameOrPasswordIncorrectException : InvalidOperationException
{
    public UsernameOrPasswordIncorrectException(string massage): base(massage) { }
}