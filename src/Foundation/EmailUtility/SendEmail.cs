using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using Sitecore;

namespace EmailUtility
{
    public class SendEmail
    {
        const string FromEmail = "techhooters@gmail.com";
        const string Subject = "New Registration on Tech Hooters";
        const string SMTP = "smtp.gmail.com";
        const string Pwd = "Hooters@2019";
        const string Port = "587";

        public void SendEmailToUser(string ToEmail,string ContentBody)
        {
          
            //string FromEmail = Sitecore.Configuration.Settings.GetSetting("FromEmail");
            //string Subject = Sitecore.Configuration.Settings.GetSetting("Subject");
            //string SMTP = Sitecore.Configuration.Settings.GetSetting("SMTPHost");
            //string Pwd = Sitecore.Configuration.Settings.GetSetting("Password");
            //string Port = Sitecore.Configuration.Settings.GetSetting("Port");
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(FromEmail);
                mail.To.Add(ToEmail);
                mail.Subject = Subject;
                mail.Body = ContentBody;
                mail.IsBodyHtml = true;
                // mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient(SMTP, System.Convert.ToInt32(Port)))
                {
                    smtp.Credentials = new NetworkCredential(FromEmail, Pwd);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
