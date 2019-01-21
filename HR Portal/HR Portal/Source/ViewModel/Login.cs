using HR_Portal.Source.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HR_Portal.Source.ViewModel
{
    class Login
    {
        public static bool Authentication(string username)
        {
            MySql mySql = new MySql();
            if (mySql.Bind("SELECT count(id) FROM users WHERE username='" + username + "' AND validitas = 1"))
            {
                Session.UserData = UserData.GetOwnDatas();
                DateTime dateTime = DateTime.Now;
                mySql.Execute("UPDATE users SET belepve = '" + dateTime.ToString("yyyy.MM.dd") + "' WHERE username = '" + username + "';");
                mySql.Close();
                return true;
            }
            else
            {
                mySql.Close();
            }
            return false;
        }

        public static string GetSavedUser()
        {
            string user;
            try
            {
                user = SqLite.Query("select username from app");
            }
            catch (Exception)
            {
                SqLite.Update("CREATE TABLE IF NOT EXISTS 'app' ('username' TEXT);");
                user = SqLite.Query("SELECT 'username' FROM 'app';");
            }
            MySql mySql = new MySql();
            mySql.Close();
            return user;
        }

        //public static void SaveUser(string username) //javítva használja: login
        //{
        //    SqLite.Update("DELETE FROM 'app';");
        //    SqLite.Update("INSERT INTO 'app' (username) VALUES ('" + username + "');");
        //    MySql.Close();
        //}

        //public static void DeleteSavedUser() //javítva használja: login
        //{
        //    SqLite.Update("DELETE FROM 'app';");
        //    MySql.Close();
        //}

        public static void Registration(string username, string name, string email, int kategoria)
        {
            MySql mySql = new MySql();
            DateTime dateTime = DateTime.Now;
            mySql.Execute("INSERT INTO `users` (`id`, `username`, `name`, `email`, `kategoria`, `jogosultsag`, `validitas`, `belepve`, `reg_datum`) VALUES (NULL, '" + username + "', '" + name + "', '" + email + "', '" + kategoria + "', '1', '1', '" + dateTime.ToString("yyyy.MM.dd") + "', '" + dateTime.ToString("yyyy.MM.dd") + "');");
            mySql.Close();
        }
        
        public List<ModelUserData> Data_UserSession(string username)  //javítva használja: login
        {
            MySql mySql = new MySql();
            MySqlDataReader sdr;
            List<ModelUserData> list = new List<ModelUserData>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand("SELECT * FROM users WHERE username='" + username + "'", mySql.conn);
                sdr = mySql.cmd.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(new ModelUserData
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        username = sdr["username"].ToString(),
                        name = sdr["name"].ToString(),
                        email = sdr["email"].ToString(),
                        kategoria = Convert.ToInt32(sdr["kategoria"]),
                        jogosultsag = Convert.ToInt32(sdr["jogosultsag"]),
                        validitas = Convert.ToInt32(sdr["validitas"]),
                        belepve = sdr["belepve"].ToString(),
                        reg_datum = sdr["reg_datum"].ToString(),
                    });
                }
                sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}
