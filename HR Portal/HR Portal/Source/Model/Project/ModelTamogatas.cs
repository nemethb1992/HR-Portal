using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Project
{
    public class ModelTamogatas
    {
        public int tamogatom { get; set; }

        public static List<ModelTamogatas> GetModelTamogatas(string command)
        {
            MySql mySql = new MySql();
            List<ModelTamogatas> list = new List<ModelTamogatas>();

            if (mySql.Open() == true)
            {
                try
                {
                    mySql.cmd = new MySqlCommand(command, mySql.conn);
                    mySql.sdr = mySql.cmd.ExecuteReader();
                    while (mySql.sdr.Read())
                    {
                        list.Add(new ModelTamogatas
                        {
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
