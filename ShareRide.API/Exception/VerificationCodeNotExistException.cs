namespace ShareRide.API.Exception
{
    public class VerificationCodeNotExistException : System.Exception
    {
        public VerificationCodeNotExistException(string massage):base(massage) { }
    }
}
