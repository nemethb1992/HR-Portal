using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Project
{
    public class ModelProjectList
    {
        public int id { get; set; }
        public string megnevezes_projekt { get; set; }
        public string megnevezes_munka { get; set; }
        public int jeloltek_db { get; set; }
        public int interjuk_db { get; set; }
        public int statusz { get; set; }
        public string fel_datum { get; set; }
        public int Completion { get; set; }

        public static List<ModelProjectList> GetModelProjectList(string command)
        {
            List<ModelProjectList> list = new List<ModelProjectList>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    int jelolt;
                    try
                    {
                        jelolt = Convert.ToInt32(MySql.sdr["jeloltek_db"]);
                    }
                    catch (Exception)
                    {
                        jelolt = 0;
                    }
                    list.Add(new ModelProjectList
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        megnevezes_projekt = MySql.sdr["megnevezes_projekt"].ToString(),
                        megnevezes_munka = MySql.sdr["megnevezes_munka"].ToString(),
                        interjuk_db = Convert.ToInt32(MySql.sdr["interjuk_db"]),
                        statusz = Convert.ToInt32(MySql.sdr["statusz"]),
                        jeloltek_db = jelolt,
                        fel_datum = MySql.sdr["fel_datum"].ToString(),
                        Completion = 100
                    });
                }
                MySql.sdr.Close();
            }
            MySql.Close();
            return list;
        }
    }
}
