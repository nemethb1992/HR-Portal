using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using HR_Portal.Source.Model;

namespace HR_Portal.Source

{
    class VMSession
    {
        private static List<UserSessionData> Datas = new List<UserSessionData>();
        
        MySql mySql = new MySql();
        Session session = new Session();

        public VMSession()
        {
            Session.UserData = setUserSession(Session.ActiveDirectoryDomain);
        }

        public static List<UserSessionData> setUserSession(string username)
        {
            List<UserSessionData> list = new List<UserSessionData>();
            MySqlDataReader sdr;
            if (MySql.open() == true)
            {
                MySql.cmd = new MySqlCommand("SELECT* FROM users WHERE username = '" + username + "'", MySql.conn);
                sdr = MySql.cmd.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(new UserSessionData
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
            MySql.close();

            return list;
        }

        public List<UserSessionData> getDatas()
        {
            List<UserSessionData> list = Session.UserData;
            return list;
        }
    }
}
