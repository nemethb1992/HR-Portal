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
            string command = @"SELECT (SELECT COUNT(*) FROM jeloltek left join munkakor on jeloltek.munkakor = munkakor.id WHERE munkakor.id = 16 AND reg_date BETWEEN '"+from+"' AND '"+to+ @"') as szerelo,
(SELECT COUNT(*) FROM jeloltek left join munkakor on jeloltek.munkakor = munkakor.id WHERE munkakor.id = 3 AND reg_date BETWEEN '" + from + "' AND '" + to + @"') as raktar,
(SELECT COUNT(*) FROM jeloltek left join munkakor on jeloltek.munkakor = munkakor.id WHERE munkakor.id = 9 AND reg_date BETWEEN '" + from + "' AND '" + to + @"') as qs,
(SELECT COUNT(*) FROM jeloltek left join munkakor on jeloltek.munkakor = munkakor.id WHERE munkakor.id IN(2,6,7,8,12,20,21) AND reg_date BETWEEN '" + from + "' AND '" + to + @"')as szellemi FROM jeloltek  LIMIT 1";

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
