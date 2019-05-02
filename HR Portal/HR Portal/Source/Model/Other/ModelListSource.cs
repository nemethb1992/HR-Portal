using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Other
{
    public class ModelProjectSmallList
    {
        public int id { get; set; }
        public string megnevezes_projekt { get; set; }

        public List<ModelProjectSmallList> GetModelProjectSmallList(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelProjectSmallList> list = new List<ModelProjectSmallList>();

            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                int j = 0;
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelProjectSmallList
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        megnevezes_projekt = mySql.sdr["megnevezes_projekt"].ToString()
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
