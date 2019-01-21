using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Project
{
    public class ModelInterview
    {
        public int id { get; set; }
        public string projekt_megnevezes { get; set; }
        public string jelolt_megnevezes { get; set; }
        public string jelolt_email { get; set; }
        public int projekt_id { get; set; }
        public int jelolt_id { get; set; }
        public int hr_id { get; set; }
        public string felvitel_datum { get; set; }
        public string interju_datum { get; set; }
        public string interju_cim { get; set; }
        public string interju_leiras { get; set; }
        public string helyszin { get; set; }
        public string idopont { get; set; }

        public static List<ModelInterview> GetModelInterview(string command)
        {
            MySql mySql = new MySql();
            List<ModelInterview> list = new List<ModelInterview>();

            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelInterview
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        projekt_megnevezes = mySql.sdr["megnevezes_projekt"].ToString(),
                        jelolt_megnevezes = mySql.sdr["nev"].ToString(),
                        jelolt_email = mySql.sdr["email"].ToString(),
                        projekt_id = Convert.ToInt32(mySql.sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(mySql.sdr["jelolt_id"]),
                        hr_id = Convert.ToInt32(mySql.sdr["hr_id"]),
                        felvitel_datum = mySql.sdr["felvitel_datum"].ToString(),
                        interju_datum = mySql.sdr["interju_datum"].ToString(),
                        interju_cim = mySql.sdr["interju_cim"].ToString(),
                        interju_leiras = mySql.sdr["interju_leiras"].ToString(),
                        helyszin = mySql.sdr["helyszin"].ToString(),
                        idopont = mySql.sdr["idopont"].ToString()
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }

        public static List<ModelInterview> GetSzakmaiInterview(string command)
        {
            MySql mySql = new MySql();
            List<ModelInterview> list = new List<ModelInterview>();

            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelInterview
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        projekt_megnevezes = mySql.sdr["megnevezes_projekt"].ToString(),
                        projekt_id = Convert.ToInt32(mySql.sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(mySql.sdr["jelolt_id"]),
                        jelolt_megnevezes = mySql.sdr["nev"].ToString(),
                        interju_datum = mySql.sdr["interju_datum"].ToString(),
                        interju_cim = mySql.sdr["interju_cim"].ToString(),
                        helyszin = mySql.sdr["helyszin"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}
