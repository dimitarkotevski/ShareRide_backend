using System.Net;
using System.Net.Mail;

namespace ShareRide.API.Config;

public class EmailConfig
{
    //private MailAddress _from { get;set } = "dimitar.kotevski1221@gmail.com";
    private MailMessage _message;
    public EmailConfig(MailMessage message)
    {
        _message = message; 
    }

    public void sendRegisterMail(string mailTo,string token)
    {
        _message.From=new MailAddress("dimitar.kotevski1221@gmail.com");
        _message.To.Add(new MailAddress(mailTo));
        _message.Subject = "Confirmation code";
        _message.Body = "Hello sir\nYour confirmation code: " + token+"\n\n"+"Best regards!";
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("dimitar.kotevski1221@gmail.com", "wxwgniglkkpfgeei"),
            EnableSsl = true
        };
        smtpClient.Send(_message);
    }
    
    public void sendForgotPasswordMail(string mailTo,string token)
    {
        _message.Subject = "Confirmation code";
        _message.Body = "Your confirmation code: " + token+"\n"+"Kind regards!";
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("dimitar.kotevski1221@gmail.com","Ddimitar123!"),
            EnableSsl = true
        };
        smtpClient.Send(_message);
    }
    
    
}