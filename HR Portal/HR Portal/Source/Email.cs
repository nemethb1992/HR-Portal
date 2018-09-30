﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Windows;
using ActiveUp.Net.Mail;
using HR_Portal.Public.templates;
using HR_Portal.Source.Model.Other;

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

            public IEnumerable<Message> GetAllMails(string mailBox)
            {
                return GetMails(mailBox, "ALL").Cast<Message>();
            }

            public IEnumerable<Message> GetUnreadMails(string mailBox)
            {
                return GetMails(mailBox, "UNSEEN").Cast<Message>();
            }

            protected Imap4Client Client
            {
                get { return client ?? (client = new Imap4Client()); }
            }

            private MessageCollection GetMails(string mailBox, string searchPhrase)
            {
                Mailbox mails = Client.SelectMailbox(mailBox);
                MessageCollection messages = mails.SearchParse(searchPhrase);
                return messages;
            }
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

        public static void Send(string to, string email_body)
        {
            List<ModelEmail> li = SMTP_List();
            try
            {
                MailMessage mail = new MailMessage();

                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient(li[0].mailserver);
                SmtpServer.Port = li[0].port;
                SmtpServer.Credentials = new NetworkCredential(li[0].login, "pmhr2018");
                SmtpServer.EnableSsl = true;

                mail.From = new MailAddress(li[0].login);
                mail.To.Add(to);
                mail.Subject = "HR Portal - Phoenix Mecano Kecskemét kft.";
                mail.Body = email_body;
                mail.IsBodyHtml = true;

                SmtpServer.SendMailAsync(mail);
            }
            catch
            {
                MessageBox.Show("Üzenet elküldése sikertelen!");
            }
        }
    }
}
