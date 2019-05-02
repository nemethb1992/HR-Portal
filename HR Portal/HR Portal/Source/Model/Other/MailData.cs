using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Other
{
    class MailData
    {
        public int id { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public int hr_id { get; set; }
        public int state { get; set; }
        public string date { get; set; }

        public static List<MailData> GetMails()
        {
            List<MailData> list = new List<MailData>();
            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                string command = "SELECT * FROM email_storage WHERE state=0";
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();

                while (mySql.sdr.Read())
                {
                    list.Add(new MailData
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        to = mySql.sdr["to"].ToString(),
                        subject = mySql.sdr["subject"].ToString(),
                        content = mySql.sdr["content"].ToString(),
                        hr_id = Convert.ToInt32(mySql.sdr["hr_id"]),
                        state = Convert.ToInt32(mySql.sdr["state"]),
                        date = mySql.sdr["date"].ToString()
                    });
                }
                mySql.sdr.Close();
                mySql.Close();
            }
            return list;
        }
        

        public void SetSent()
        {
            state = 1;
            MySqlDB mySql = new MySqlDB();
            mySql.Execute("UPDATE `email_storage` SET `state` = 1 WHERE `id` = " + id + ";");
        }
    }
}
