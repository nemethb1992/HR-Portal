
using HR_Portal.Public.templates;
using System;

namespace HR_Portal.Source
{
    class Email
    {
        EmailTemplate emailTemplate = new EmailTemplate();

        public void Send(string to, string email_body)
        {
            MySql mySql = new MySql();
            string sql = "INSERT INTO `email_storage` (`to`, `subject`, `content`, `hr_id`, `state`, `date`) VALUES ('" + to + "', 'HR Portal -Phoenix Mecano Kecskemét kft.', '"+ email_body +"', " + Session.UserData.id + ", 0, '"+ DateTime.Now.ToString("yyyy.MM.dd") + "');";
            mySql.Execute(sql);
        }
    }
}


//var fromAddress = new MailAddress("hrportal@pm-hungaria.hu", "From Name");
//var toAddress = new MailAddress(to, "To Name");
//const string fromPassword = "pmhr2018";
//const string subject = "Phoenix teszt";
//string body = email_body;

//var smtp = new System.Net.Mail.SmtpClient
//{
//    Host = "smtp.gmail.com",
//    Port = 587,
//    EnableSsl = true,
//    DeliveryMethod = SmtpDeliveryMethod.Network,
//    UseDefaultCredentials = false,
//    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
//};
//using (var message = new MailMessage(fromAddress, toAddress)
//{
//    Subject = subject,
//    IsBodyHtml = true,
//    Body = body
//})
//{
//    smtp.Send(message);
//}

//try
//{
//    using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient())
//    {
//        client.Host = "192.168.144.14";
//        client.Port = 25;
//        client.UseDefaultCredentials = true;
//        //client.Credentials = new NetworkCredential("hrportal@phoenix-mecano.hu", "pmhr2018!");


//        using (MailMessage mail = new MailMessage())
//        {
//            mail.Subject = "HR Portal - Phoenix Mecano Kecskemét kft.";
//            mail.Body = email_body;
//            mail.From = new MailAddress(Session.UserData.email);
//            //mail.From = new MailAddress("hrportal@phoenix-mecano.hu");
//            mail.IsBodyHtml = true;
//            mail.To.Add(to);
//            client.Send(mail);
//        }
//    }
//}
//catch (System.Net.Mail.SmtpException ex)
//{
//    MessageBox.Show("Üzenet küldése sikertelen!\n\n" + ex);
//}