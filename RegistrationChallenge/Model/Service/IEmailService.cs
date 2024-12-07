namespace RegistrationChallenge.Model.Service
{
    // IEmailService - описание сервиса для  работы с электронной почтой
    public interface IEmailService
    {
        // SendMailAsync - отправка письма по электронной почте
        Task SendMailAsync(string email, string subject, string content);
    }
}
