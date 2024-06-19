using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NotificationApplication
{
    public class EmailSenderService : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Виктор Дычок", "vitya.dychok@mail.ru"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Timeout = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;
                    await client.ConnectAsync("smtp.mail.ru", 587, false);
                    await client.AuthenticateAsync("vitya.dychok@mail.ru", "qG5fVzbwL0Sn3jJ6HsGt");
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при отправке почты" + e.Message);
            }

        }
    }
}
