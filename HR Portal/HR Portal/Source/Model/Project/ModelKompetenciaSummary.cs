using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Project
{
    public class ModelKompetenciaSummary
    {
        public int k1_val { get; set; }
        public int k2_val { get; set; }
        public int k3_val { get; set; }
        public int k4_val { get; set; }
        public int k5_val { get; set; }
        public int tamogatom { get; set; }

        public static List<ModelKompetenciaSummary> GetModelKompetenciaSummary(string command)
        {
            MySql mySql = new MySql();
            List<ModelKompetenciaSummary> list = new List<ModelKompetenciaSummary>();

            if (mySql.Open() == true)
            {
                try
                {
                    mySql.cmd = new MySqlCommand(command, mySql.conn);
                    mySql.sdr = mySql.cmd.ExecuteReader();
                    while (mySql.sdr.Read())
                    {
                        list.Add(new ModelKompetenciaSummary
                        {
                            k1_val = Convert.ToInt32(mySql.sdr["k1_val"]),
                            k2_val = Convert.ToInt32(mySql.sdr["k2_val"]),
                            k3_val = Convert.ToInt32(mySql.sdr["k3_val"]),
                            k4_val = Convert.ToInt32(mySql.sdr["k4_val"]),
                            k5_val = Convert.ToInt32(mySql.sdr["k5_val"]),
                            tamogatom = Convert.ToInt32(mySql.sdr["tamogatom"]),
                        });
                    }
                }
                catch (Exception)
                {
                }

                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}
