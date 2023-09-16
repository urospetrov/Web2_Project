using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WEB2_Projekat.EmailServices;

namespace Shared.EmailServices
{
    public class EmailService: IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            //var smtpClient = new SmtpClient(_emailSettings.Host);
            //smtpClient.Port = _emailSettings.Port;
            //smtpClient.UseDefaultCredentials = false;
            //smtpClient.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
            //smtpClient.EnableSsl = true;

            //var mailMessage = new MailMessage
            //{
            //    From = new MailAddress(_emailSettings.UserName),
            //    Subject = subject,
            //    Body = message,
            //    IsBodyHtml = true
            //};
            //mailMessage.To.Add(toEmail);

            MailMessage mailMessage = new MailMessage();
            var smtpClient = new SmtpClient(_emailSettings.Host);
            mailMessage.From = new MailAddress(_emailSettings.UserName);
            mailMessage.To.Add(new MailAddress(toEmail));
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true; //to make message body as html
            mailMessage.Body = message;
            smtpClient.Port = _emailSettings.Port;
            smtpClient.Host = _emailSettings.Host;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                await smtpClient.SendMailAsync(mailMessage);

            }
            catch (Exception ex) 
            { 
            
            }
        }
    }
}
