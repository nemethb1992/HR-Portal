using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model
{
    public class ModelUserData
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int kategoria { get; set; }
        public int jogosultsag { get; set; }
        public int validitas { get; set; }
        public string belepve { get; set; }
        public string reg_datum { get; set; }

        public static List<ModelUserData> GetUserSession(string command)
        {
            List<ModelUserData> list = new List<ModelUserData>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelUserData
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        username = MySql.sdr["username"].ToString(),
                        name = MySql.sdr["name"].ToString(),
                        email = MySql.sdr["email"].ToString(),
                        kategoria = Convert.ToInt32(MySql.sdr["kategoria"]),
                        jogosultsag = Convert.ToInt32(MySql.sdr["jogosultsag"]),
                        validitas = Convert.ToInt32(MySql.sdr["validitas"]),
                        belepve = MySql.sdr["belepve"].ToString(),
                        reg_datum = MySql.sdr["reg_datum"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            MySql.Close();

            return list;
        }
    }
}
