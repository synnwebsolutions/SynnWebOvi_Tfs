using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Mail
{
    public static class MailHepler
    {
        public static bool SendMail(string from, string to, string host, string subject, string body)
        {
            MailMessage mail = new MailMessage(from, to);
            using (SmtpClient client = new SmtpClient())
            {
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = host;
                mail.Subject = subject;
                mail.Body = body;
                client.Send(mail);
            }
            return true;
        }
    }
}
