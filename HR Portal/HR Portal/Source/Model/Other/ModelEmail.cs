using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Other
{
    //public class ModelEmail
    //{
    //    public string type { get; set; }
    //    public string mailserver { get; set; }
    //    public int port { get; set; }
    //    public bool ssl { get; set; }
    //    public string login { get; set; }
    //    public string password { get; set; }

    //    public static List<ModelEmail> GetModelEmail(string command)
    //    {
    //        MySql mySql = new MySql();
    //        List<ModelEmail> items = new List<ModelEmail>();

    //        if (mySql.Open() == true)
    //        {
    //            mySql.cmd = new MySqlCommand(command, mySql.conn);
    //            mySql.sdr = mySql.cmd.ExecuteReader();
    //            while (mySql.sdr.Read())
    //            {
    //                bool ssl = true;
    //                if (Convert.ToInt32(mySql.sdr["ssl"]) == 0)
    //                {
    //                    ssl = false;
    //                }
    //                items.Add(new ModelEmail
    //                {
    //                    type = mySql.sdr["type"].ToString(),
    //                    mailserver = mySql.sdr["mailserver"].ToString(),
    //                    port = Convert.ToInt32(mySql.sdr["port"]),
    //                    ssl = ssl,
    //                    login = mySql.sdr["login"].ToString()
    //                });
    //            }
    //            mySql.sdr.Close();
    //        }
    //        mySql.Close();
    //        return items;
    //    }
    //}
}
