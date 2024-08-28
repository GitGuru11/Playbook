using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Playbook.Engine
{
    public class EmailSender
    {
        private string smtpServer;
        private int smtpPort = 587;
        private string smtpUser;
        private string smtpPass;
        public EmailSender() 
        {
            // SMTP server configuration
            this.smtpServer = "smtp.example.com";
            smtpPort = 587; 
            smtpUser = "jack.daniel@aviso.com";
            smtpPass = "S!mba1111";
        }

        public async Task SendEmailAsync(string fromEmail, List<string> toEmails, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    // Configure the client
                    client.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    client.EnableSsl = true; // Use SSL to encrypt the connection

                    // Create the email message
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false, // Set to true if the body contains HTML
                    };

                    // Add each recipient to the To list
                    foreach (var toEmail in toEmails)
                    {
                        mailMessage.To.Add(toEmail);
                    }

                    // Send the email asynchronously
                    await client.SendMailAsync(mailMessage);

                    Console.WriteLine("Email sent successfully to all recipients.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }
}
