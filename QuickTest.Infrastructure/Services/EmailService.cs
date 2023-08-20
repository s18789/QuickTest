using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string smtpServer = "smtp.gmail.com"; 
        private readonly int smtpPort = 587; 
        private readonly string smtpUser = "noreply.quicktest@gmail.com"; 
        private readonly string smtpPassword = "vzhwgidfciuxsscn"; 
        private readonly string fromEmail = "noreply.quicktest@gmail.com";
        private readonly string adminEmail = "noreply.quicktest@gmail.com";

        public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    client.EnableSsl = true; 

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false, 
                    };

                    mailMessage.To.Add(recipientEmail);
                    //mailMessage.CC.Add(adminEmail); 

                    await client.SendMailAsync(mailMessage);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
