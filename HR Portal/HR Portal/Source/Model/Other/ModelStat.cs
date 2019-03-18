using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Other
{
    class ModelStat
    {
        public int szerelo { get; set; }
        public int raktar { get; set; }
        public int qs { get; set; }
        public int szellemi { get; set; }
        public string date { get; set; }

        public static ModelStat GetModelStat1(string from, string to)
        {
            //web db-ből
            string command = @"SELECT projektek.megnevezes_projekt, count(regisztraltak.email) FROM regisztraltak 
                               LEFT JOIN projektek on projektek.id = regisztraltak.projekt_id 
                               WHERE regisztraltak.reg_date > '2019.03.10' AND regisztraltak.reg_date < '2019.03.18' 
                               GROUP BY projektek.megnevezes_projekt";

            MySql mySql = new MySql();
            ModelStat data = new ModelStat();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    data = (new ModelStat
                    {
                        szerelo = Convert.ToInt32(mySql.sdr["szerelo"]),
                        raktar = Convert.ToInt32(mySql.sdr["raktar"]),
                        qs = Convert.ToInt32(mySql.sdr["qs"]),
                        szellemi = Convert.ToInt32(mySql.sdr["szellemi"]),
                        date = from + " - " + to
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return data;
        }
    }
}
