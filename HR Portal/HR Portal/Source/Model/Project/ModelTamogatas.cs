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
            List<ModelTamogatas> list = new List<ModelTamogatas>();

            if (MySql.Open() == true)
            {
                try
                {
                    MySql.cmd = new MySqlCommand(command, MySql.conn);
                    MySql.sdr = MySql.cmd.ExecuteReader();
                    while (MySql.sdr.Read())
                    {
                        list.Add(new ModelTamogatas
                        {
                            tamogatom = Convert.ToInt32(MySql.sdr["tamogatom"]),
                        });
                    }
                }
                catch (Exception)
                {
                }

                MySql.sdr.Close();
            }
            return list;
        }
    }
}
