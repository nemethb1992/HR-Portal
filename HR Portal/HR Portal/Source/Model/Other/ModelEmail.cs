using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Other
{
    public class ModelEmail
    {
        public string type { get; set; }
        public string mailserver { get; set; }
        public int port { get; set; }
        public bool ssl { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public static List<ModelEmail> GetModelEmail(string command)
        {
            List<ModelEmail> items = new List<ModelEmail>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    bool ssl = true;
                    if (Convert.ToInt32(MySql.sdr["ssl"]) == 0)
                    {
                        ssl = false;
                    }
                    items.Add(new ModelEmail
                    {
                        type = MySql.sdr["type"].ToString(),
                        mailserver = MySql.sdr["mailserver"].ToString(),
                        port = Convert.ToInt32(MySql.sdr["port"]),
                        ssl = ssl,
                        login = MySql.sdr["login"].ToString()
                    });
                }
                MySql.sdr.Close();
            }
            return items;
        }
    }
}
