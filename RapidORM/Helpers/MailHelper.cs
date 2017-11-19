using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace RapidORM.Helpers
{
    public static class MailHelper
    {
        public static void SendEmail(string recipient, string sender, string subject, string body, string attachmentLocation = "")
        {
            using (var smtpClient = new SmtpClient())
            {
                using (var mail = new MailMessage())
                {
                    mail.To.Add(recipient);

                    mail.From = new MailAddress(sender);
                    mail.Subject = subject;
                    mail.Body = body;

                    if (attachmentLocation != "")
                    {
                        Attachment attachment = new Attachment(attachmentLocation);
                        mail.Attachments.Add(attachment);
                    }

                    smtpClient.Send(mail);
                }
            }
        }
    }
}
