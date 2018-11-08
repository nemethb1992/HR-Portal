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

        public class MailRepository
        {
            private Imap4Client client;

            public MailRepository(string mailServer, int port, bool ssl, string login, string password)
            {
                if (ssl)
                    Client.ConnectSsl(mailServer, port);
                else
                    Client.Connect(mailServer, port);
                Client.Login(login, password);
            }

            //public IEnumerable<Message> GetAllMails(string mailBox)
            //{
            //    return GetMails(mailBox, "ALL").Cast<Message>();
            //}

            //public IEnumerable<Message> GetUnreadMails(string mailBox)
            //{
            //    return GetMails(mailBox, "UNSEEN").Cast<Message>();
            //}

            protected Imap4Client Client
            {
                get { return client ?? (client = new Imap4Client()); }
            }

            //private MessageCollection GetMails(string mailBox, string searchPhrase)
            //{
            //    System.Net.Mail.Mailbox mails = Client.SelectMailbox(mailBox);
            //    MessageCollection messages = mails.SearchParse(searchPhrase);
            //    return messages;
            //}
        }
        public List<ModelEmail> IMAP_List()
        {
            string command = "SELECT * FROM ConnectionSMTP WHERE type = 'imap'";
            List<ModelEmail> list = ModelEmail.GetModelEmail(command);
            MySql.Close();
            return list;
        }
        public static List<ModelEmail> SMTP_List()
        {
            string command = "SELECT * FROM ConnectionSMTP WHERE type = 'smtp'";
            List<ModelEmail> list = ModelEmail.GetModelEmail(command);
            MySql.Close();
            return list;
        }

        public void Send(string to, string email_body)
        {
            try
            {
                //using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("192.168.144.14"))
                //{
                //    client.UseDefaultCredentials = true;

                //    using (MailMessage mail = new MailMessage())
                //    {
                //        mail.Subject = "subject";
                //        mail.Body = "body";
                //        mail.From = new MailAddress("felado@phoenix-mecano.hu");
                //        mail.To.Add("fzbalu92@gmail.com");
                //        client.Send(mail);
                //    }
                //}

                ExchangeService service = new ExchangeService();
                //service.Credentials = new WebCredentials("balazs.nemeth", "3HgB8Wy3HgB8Wy", "pmhu.local");
                service.UseDefaultCredentials = true;
                service.AutodiscoverUrl("balazs.nemeth@pmhu.local");


                EmailMessage message = new EmailMessage(service);
                message.Subject = "HR Portal - Phoenix Mecano Kecskemét kft.";

                message.Body = email_body;

                message.ToRecipients.Add(to);
                message.Send();


                //using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient())
                //{
                //    client.Credentials = new NetworkCredential("pmhu\\hrportal.pmk ", "pmhr2018!");
                //    client.Host = "192.168.144.14";
                //    client.Port = 25;
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                //    client.EnableSsl = false;

                //    using (MailMessage mail = new MailMessage())
                //    {
                //        mail.Subject = "HR Portal - Phoenix Mecano Kecskemét kft.";
                //        mail.Body = email_body;
                //        mail.From = new MailAddress("hrportal@pm-hungaria.hu");
                //        mail.To.Add(to);
                //        client.Send(mail);
                //    }
                //}
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show("Üzenet elküldése sikertelen!" + ex);
            }

            //List<ModelEmail> li = SMTP_List();
            //MailMessage mail = new MailMessage();
            //System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient(li[0].mailserver);
            //SmtpServer.Port = li[0].port;
            ////SmtpServer.Credentials = new NetworkCredential(li[0].login, "pmhr2018");
            //SmtpServer.EnableSsl = false;

            //mail.From = new MailAddress(li[0].login);
            //mail.To.Add(to);
            //mail.Subject = "HR Portal - Phoenix Mecano Kecskemét kft.";
            //mail.Body = email_body;
            //mail.IsBodyHtml = true;

            ////SmtpServer.Send(mail);
            //SmtpServer.Send(mail);
        }
    }
}
