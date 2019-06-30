
using HR_Portal.Public.templates;
using System;

namespace HR_Portal.Source
{
    class Email
    {
        EmailTemplate emailTemplate = new EmailTemplate();

        public void Send(string to, string email_body)
        {
            MySqlDB mySql = new MySqlDB();
            string sql = "INSERT INTO `email_storage` (`to`, `subject`, `content`, `hr_id`, `state`, `date`) VALUES ('" + to + "', 'HR Portal -Phoenix Mecano Kecskemét kft.', '"+ email_body.Replace("\'", "\"") + "', " + Session.UserData.id + ", 0, '"+ DateTime.Now.ToString("yyyy.MM.dd") + "');";
            mySql.Execute(sql);
        }
    }
}