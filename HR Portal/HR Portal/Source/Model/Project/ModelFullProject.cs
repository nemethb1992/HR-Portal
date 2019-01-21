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

        public static List<ModelFullProject> GetModelFullProject(string command)
        {
            MySql mySql = new MySql();
            List<ModelFullProject> list = new List<ModelFullProject>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    int jelolt;
                    try
                    {
                        jelolt = Convert.ToInt32(mySql.sdr["jeloltek_db"]);
                    }
                    catch (Exception)
                    {
                        jelolt = 0;
                    }
                    list.Add(new ModelFullProject
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        hr_id = Convert.ToInt32(mySql.sdr["hr_id"]),
                        megnevezes_projekt = mySql.sdr["megnevezes_projekt"].ToString(),
                        megnevezes_vegzettseg = mySql.sdr["megnevezes_vegzettseg"].ToString(),
                        megnevezes_nyelv = mySql.sdr["megnevezes_nyelv"].ToString(),
                        megnevezes_munka = mySql.sdr["megnevezes_munka"].ToString(),
                        megnevezes_pc = mySql.sdr["megnevezes_pc"].ToString(),
                        megnevezes_hr = mySql.sdr["name"].ToString(),
                        fel_datum = mySql.sdr["fel_datum"].ToString(),
                        le_datum = mySql.sdr["le_datum"].ToString(),
                        pc = Convert.ToInt32(mySql.sdr["pc"]),
                        vegzettseg = Convert.ToInt32(mySql.sdr["vegzettseg"]),
                        tapasztalat_ev = Convert.ToInt32(mySql.sdr["tapasztalat_ev"]),
                        statusz = mySql.sdr["allapot"].ToString(),
                        publikalt = Convert.ToInt32(mySql.sdr["publikalt"]),
                        nyelvtudas = Convert.ToInt32(mySql.sdr["nyelvtudas"]),
                        munkakor = Convert.ToInt32(mySql.sdr["munkakor"]),
                        szuldatum = Convert.ToInt32(mySql.sdr["szuldatum"]),
                        ber = Convert.ToInt32(mySql.sdr["ber"]),
                        jeloltek_db = jelolt,
                        kepesseg1 = Convert.ToInt32(mySql.sdr["kepesseg1"]),
                        kepesseg2 = Convert.ToInt32(mySql.sdr["kepesseg2"]),
                        kepesseg3 = Convert.ToInt32(mySql.sdr["kepesseg3"]),
                        kepesseg4 = Convert.ToInt32(mySql.sdr["kepesseg4"]),
                        kepesseg5 = Convert.ToInt32(mySql.sdr["kepesseg5"]),
                        feladatok = mySql.sdr["feladatok"].ToString(),
                        elvarasok = mySql.sdr["elvarasok"].ToString(),
                        kinalunk = mySql.sdr["kinalunk"].ToString(),
                        elonyok = mySql.sdr["elonyok"].ToString(),
                    });

                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}
