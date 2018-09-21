using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Project
{
    public class ModelFullProject
    {
        public int id { get; set; }
        public int hr_id { get; set; }
        public string megnevezes_projekt { get; set; }
        public string megnevezes_vegzettseg { get; set; }
        public string megnevezes_nyelv { get; set; }
        public string megnevezes_munka { get; set; }
        public string megnevezes_pc { get; set; }
        public string megnevezes_hr { get; set; }
        public string fel_datum { get; set; }
        public string le_datum { get; set; }
        public int pc { get; set; }
        public int vegzettseg { get; set; }
        public int tapasztalat_ev { get; set; }
        public string statusz { get; set; }
        public int publikalt { get; set; }
        public int nyelvtudas { get; set; }
        public int munkakor { get; set; }
        public int szuldatum { get; set; }
        public int ber { get; set; }
        public int jeloltek_db { get; set; }
        public int kepesseg1 { get; set; }
        public int kepesseg2 { get; set; }
        public int kepesseg3 { get; set; }
        public int kepesseg4 { get; set; }
        public int kepesseg5 { get; set; }
        public string feladatok { get; set; }
        public string elvarasok { get; set; }
        public string kinalunk { get; set; }
        public string elonyok { get; set; }

        public static List<ModelFullProject> getModelFullProject(string command)
        {
            List<ModelFullProject> list = new List<ModelFullProject>();
            if (MySql.open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    int jelolt;
                    try
                    {
                        jelolt = Convert.ToInt32(MySql.sdr["jeloltek_db"]);
                    }
                    catch (Exception)
                    {
                        jelolt = 0;
                    }
                    list.Add(new ModelFullProject
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        hr_id = Convert.ToInt32(MySql.sdr["hr_id"]),
                        megnevezes_projekt = MySql.sdr["megnevezes_projekt"].ToString(),
                        megnevezes_vegzettseg = MySql.sdr["megnevezes_vegzettseg"].ToString(),
                        megnevezes_nyelv = MySql.sdr["megnevezes_nyelv"].ToString(),
                        megnevezes_munka = MySql.sdr["megnevezes_munka"].ToString(),
                        megnevezes_pc = MySql.sdr["megnevezes_pc"].ToString(),
                        megnevezes_hr = MySql.sdr["name"].ToString(),
                        fel_datum = MySql.sdr["fel_datum"].ToString(),
                        le_datum = MySql.sdr["le_datum"].ToString(),
                        pc = Convert.ToInt32(MySql.sdr["pc"]),
                        vegzettseg = Convert.ToInt32(MySql.sdr["vegzettseg"]),
                        tapasztalat_ev = Convert.ToInt32(MySql.sdr["tapasztalat_ev"]),
                        statusz = MySql.sdr["allapot"].ToString(),
                        publikalt = Convert.ToInt32(MySql.sdr["publikalt"]),
                        nyelvtudas = Convert.ToInt32(MySql.sdr["nyelvtudas"]),
                        munkakor = Convert.ToInt32(MySql.sdr["munkakor"]),
                        szuldatum = Convert.ToInt32(MySql.sdr["szuldatum"]),
                        ber = Convert.ToInt32(MySql.sdr["ber"]),
                        jeloltek_db = jelolt,
                        kepesseg1 = Convert.ToInt32(MySql.sdr["kepesseg1"]),
                        kepesseg2 = Convert.ToInt32(MySql.sdr["kepesseg2"]),
                        kepesseg3 = Convert.ToInt32(MySql.sdr["kepesseg3"]),
                        kepesseg4 = Convert.ToInt32(MySql.sdr["kepesseg4"]),
                        kepesseg5 = Convert.ToInt32(MySql.sdr["kepesseg5"]),
                        feladatok = MySql.sdr["feladatok"].ToString(),
                        elvarasok = MySql.sdr["elvarasok"].ToString(),
                        kinalunk = MySql.sdr["kinalunk"].ToString(),
                        elonyok = MySql.sdr["elonyok"].ToString(),
                    });

                }
                MySql.sdr.Close();
            }
            return list;
        }
    }
}
