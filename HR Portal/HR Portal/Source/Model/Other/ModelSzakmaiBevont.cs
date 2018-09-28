using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source
{
    public class ModelSzakmaiBevont
    {
        public int id { get; set; }
        public string megnevezes_projekt { get; set; }
        public string megnevezes_munka { get; set; }
        public int jeloltek_db { get; set; }
    
        public static List<ModelSzakmaiBevont> GetModelSzakmaiBevont(string command)
        {
            List<ModelSzakmaiBevont> list = new List<ModelSzakmaiBevont>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelSzakmaiBevont
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        megnevezes_projekt = MySql.sdr["megnevezes_projekt"].ToString(),
                        megnevezes_munka = MySql.sdr["megnevezes_munka"].ToString(),
                        jeloltek_db = Convert.ToInt32(MySql.sdr["jeloltek_db"]),

                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }


}
