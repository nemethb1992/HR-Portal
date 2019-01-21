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
            MySql mySql = new MySql();
            List<ModelSzakmaiBevont> list = new List<ModelSzakmaiBevont>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelSzakmaiBevont
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        megnevezes_projekt = mySql.sdr["megnevezes_projekt"].ToString(),
                        megnevezes_munka = mySql.sdr["megnevezes_munka"].ToString(),
                        jeloltek_db = Convert.ToInt32(mySql.sdr["jeloltek_db"]),

                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }


}
