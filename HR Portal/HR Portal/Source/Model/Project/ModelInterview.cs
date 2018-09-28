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
            List<ModelInterview> list = new List<ModelInterview>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelInterview
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        projekt_megnevezes = MySql.sdr["megnevezes_projekt"].ToString(),
                        jelolt_megnevezes = MySql.sdr["nev"].ToString(),
                        jelolt_email = MySql.sdr["email"].ToString(),
                        projekt_id = Convert.ToInt32(MySql.sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(MySql.sdr["jelolt_id"]),
                        hr_id = Convert.ToInt32(MySql.sdr["hr_id"]),
                        felvitel_datum = MySql.sdr["felvitel_datum"].ToString(),
                        interju_datum = MySql.sdr["interju_datum"].ToString(),
                        interju_cim = MySql.sdr["interju_cim"].ToString(),
                        interju_leiras = MySql.sdr["interju_leiras"].ToString(),
                        helyszin = MySql.sdr["helyszin"].ToString(),
                        idopont = MySql.sdr["idopont"].ToString()
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }

        public static List<ModelInterview> GetSzakmaiInterview(string command)
        {
            List<ModelInterview> list = new List<ModelInterview>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelInterview
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        projekt_megnevezes = MySql.sdr["megnevezes_projekt"].ToString(),
                        projekt_id = Convert.ToInt32(MySql.sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(MySql.sdr["jelolt_id"]),
                        jelolt_megnevezes = MySql.sdr["nev"].ToString(),
                        interju_datum = MySql.sdr["interju_datum"].ToString(),
                        interju_cim = MySql.sdr["interju_cim"].ToString(),
                        helyszin = MySql.sdr["helyszin"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }
}
