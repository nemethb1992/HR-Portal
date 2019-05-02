using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Other
{
    public class ModelErrorLog
    {
        public int id { get; set; }
        public string placeofbug { get; set; }
        public string description { get; set; }
        public string solution { get; set; }
        public string date { get; set; }
        public string result { get; set; }
        public string resultdate { get; set; }

        public List<ModelErrorLog> Get(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelErrorLog> list = new List<ModelErrorLog>();

            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                int j = 0;
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelErrorLog
                    {
                        id = (Convert.ToInt32(mySql.sdr["id"])),
                        placeofbug = mySql.sdr["placeofbug"].ToString(),
                        description = mySql.sdr["description"].ToString(),
                        solution = mySql.sdr["solution"].ToString(),
                        date = mySql.sdr["date"].ToString(),
                        result = mySql.sdr["result"].ToString(),
                        resultdate = mySql.sdr["resultdate"].ToString()
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
