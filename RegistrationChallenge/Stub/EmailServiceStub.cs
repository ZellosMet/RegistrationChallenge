using RegistrationChallenge.Model.Service;

namespace RegistrationChallenge.Stub
{
    public class EmailServiceStub : IEmailService
    {
        public async Task SendMailAsync(string email, string subject, string content)
        {
            Console.WriteLine("SendEmail stub");
            Console.WriteLine("Receiver: " + email);
            Console.WriteLine("Subject: " + subject);
            Console.WriteLine("Content: " + content);
            await Task.Run(() => { });
        }
    }
}
