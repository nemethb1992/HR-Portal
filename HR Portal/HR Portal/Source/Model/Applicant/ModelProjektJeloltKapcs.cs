using HR_Portal.Source;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal_Test.Source.Model.Applicant
{
    class ModelProjektJeloltKapcs
    {
        public int id { get; set; }
        public int projekt_id { get; set; }
        public int jelolt_id { get; set; }
        public string datum { get; set; }

        public static List<ModelProjektJeloltKapcs> Get(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelProjektJeloltKapcs> list = new List<ModelProjektJeloltKapcs>();

            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelProjektJeloltKapcs
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        projekt_id = Convert.ToInt32(mySql.sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(mySql.sdr["jelolt_id"]),
                        datum = mySql.sdr["datum"].ToString(),

                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}
