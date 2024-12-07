using RegistrationChallenge.Model.Service;
using System.Net;
using System.Net.Mail;

namespace RegistrationChallenge.Model.Email
{
    public class SendEmailService : IEmailService
    {
        public async Task SendMailAsync(string to_email, string topic, string message)
        {           
            //Создаём подключене к почтовому сервису и настраиваем его
            using (SmtpClient client = new SmtpClient("smpt.mail.ru", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("ptr_ivanovich_84@mail.ru", "0qduHtncu8HaW1yLx8Kb")
            })
            {
                //Отправляем письмо согласно указанных параметров
                await client.SendMailAsync("ptr_ivanovich_84@mail.ru", to_email, topic, message);                    
            }           
        }
    }
}
