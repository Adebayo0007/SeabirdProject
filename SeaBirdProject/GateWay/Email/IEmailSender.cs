namespace SeaBirdProject.GateWay.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailRequestModel email);
        Task<bool> EmailValidaton(string email);
    }
}
