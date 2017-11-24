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
        /// <summary>
        /// Sends user defined email
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="sender"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachmentLocation"></param>
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
