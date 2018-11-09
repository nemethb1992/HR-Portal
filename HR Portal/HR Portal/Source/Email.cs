using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Windows;
using ActiveUp.Net.Mail;
using HR_Portal.Public.templates;
using HR_Portal.Source.Model.Other;
using System;
using Microsoft.Exchange.WebServices.Data;

namespace HR_Portal.Source
{
    class Email
    {
        EmailTemplate emailTemplate = new EmailTemplate();

        public void Send(string to, string email_body)
        {
            try
            {
                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient())
                {
                    client.Host = "192.168.144.14";
                    client.Port = 25;
                    client.UseDefaultCredentials = true;


                    using (MailMessage mail = new MailMessage())
                    {
                        mail.Subject = "HR Portal - Phoenix Mecano Kecskemét kft.";
                        mail.Body = email_body;
                        mail.From = new MailAddress(Session.UserData[0].email);
                        mail.IsBodyHtml = true;
                        mail.To.Add(to);
                        client.Send(mail);
                    }
                }
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show("Üzenet küldése sikertelen!\n\n" + ex);
            }
        }
    }
}
