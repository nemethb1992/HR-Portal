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
            List<ModelErrorLog> list = new List<ModelErrorLog>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                int j = 0;
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelErrorLog
                    {
                        id = (Convert.ToInt32(MySql.sdr["id"])),
                        placeofbug = MySql.sdr["placeofbug"].ToString(),
                        description = MySql.sdr["description"].ToString(),
                        solution = MySql.sdr["solution"].ToString(),
                        date = MySql.sdr["date"].ToString(),
                        result = MySql.sdr["result"].ToString(),
                        resultdate = MySql.sdr["resultdate"].ToString()
                    });
                    j++;
                }
                MySql.sdr.Close();
            }
            MySql.Close();
            return list;
        }
    }



}
