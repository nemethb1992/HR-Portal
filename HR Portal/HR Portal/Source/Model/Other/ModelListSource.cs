using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Other
{
    class ModelListSource
    {

    }
    public class ModelProjectSmallList
    {
        public int id { get; set; }
        public string megnevezes_projekt { get; set; }

        public List<ModelProjectSmallList> GetModelProjectSmallList(string command)
        {
            List<ModelProjectSmallList> list = new List<ModelProjectSmallList>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                int j = 0;
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelProjectSmallList
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        megnevezes_projekt = MySql.sdr["megnevezes_projekt"].ToString()
                    });
                    j++;
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }
}
