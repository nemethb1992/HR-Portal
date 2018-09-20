using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HR_Portal.Source

{
    class VMSession
    {
        private static List<UserSessionData> Datas = new List<UserSessionData>();
        
        MySql mySql = new MySql();

        public VMSession()
        {

        }
        public static void setUserSession(string username)
        {
            MySqlDataReader sdr;
            if (MySql.open() == true)
            {
                MySql.cmd = new MySqlCommand("SELECT* FROM users WHERE username = '" + username + "'", MySql.conn);
                sdr = MySql.cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Datas.Add(new UserSessionData
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
        }
        public static List<UserSessionData> getDatas(string username)
        {
            List<UserSessionData> list = Datas;
            return list;
        }
    }
}
