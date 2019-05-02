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

        public static List<ModelSmallProject> GetModelSmallProject(string command)
        {
            List<ModelSmallProject> list = new List<ModelSmallProject>();
            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                int j = 0;
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelSmallProject
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        megnevezes_projekt = mySql.sdr["megnevezes_projekt"].ToString(),
                        jeloltek_db = 0
                    });
                    j++;
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}
