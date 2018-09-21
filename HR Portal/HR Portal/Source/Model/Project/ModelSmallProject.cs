using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Project
{
    public class ModelSmallProject
    {
        public int id { get; set; }
        public string megnevezes_projekt { get; set; }
        public int jeloltek_db { get; set; }

        public static List<ModelSmallProject> getModelSmallProject(string command)
        {
            List<ModelSmallProject> list = new List<ModelSmallProject>();

            if (MySql.open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                int j = 0;
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelSmallProject
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        megnevezes_projekt = MySql.sdr["megnevezes_projekt"].ToString(),
                        jeloltek_db = 0
                    });
                    j++;
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }
}
