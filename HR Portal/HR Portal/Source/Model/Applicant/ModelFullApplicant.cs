using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Applicant
{
    public class ModelFullApplicant
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string email { get; set; }
        public string telefon { get; set; }
        public string lakhely { get; set; }
        public string ertesult { get; set; }
        public int id_ertesult { get; set; }
        public int szuldatum { get; set; }
        public string neme { get; set; }
        public int id_neme { get; set; }
        public int tapasztalat_ev { get; set; }
        public string munkakor { get; set; }
        public string munkakor2 { get; set; }
        public string munkakor3 { get; set; }
        public int id_munkakor { get; set; }
        public int id_munkakor2 { get; set; }
        public int id_munkakor3 { get; set; }
        public string vegz_terulet { get; set; }
        public int id_vegz_terulet { get; set; }
        public string nyelvtudas { get; set; }
        public string nyelvtudas2 { get; set; }
        public int id_nyelvtudas { get; set; }
        public int id_nyelvtudas2 { get; set; }
        public string reg_date { get; set; }
        public string megjegyzes { get; set; }
        public string folderUrl { get; set; }

        public static List<ModelFullApplicant> GetModelFullApplicant(string command)
        {
            List<ModelFullApplicant> list = new List<ModelFullApplicant>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelFullApplicant
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        nev = MySql.sdr["nev"].ToString(),
                        email = MySql.sdr["email"].ToString(),
                        telefon = MySql.sdr["telefon"].ToString(),
                        lakhely = MySql.sdr["lakhely"].ToString(),
                        ertesult = MySql.sdr["ertesules_megnevezes"].ToString(),
                        id_ertesult = Convert.ToInt32(MySql.sdr["id_ertesult"]),
                        szuldatum = Convert.ToInt32(MySql.sdr["szuldatum"]),
                        neme = MySql.sdr["neme"].ToString(),
                        id_neme = Convert.ToInt32(MySql.sdr["id_neme"]),
                        tapasztalat_ev = Convert.ToInt32(MySql.sdr["tapasztalat_ev"]),
                        munkakor = MySql.sdr["munkakor"].ToString(),
                        munkakor2 = MySql.sdr["munkakor2"].ToString(),
                        munkakor3 = MySql.sdr["munkakor3"].ToString(),
                        id_munkakor = Convert.ToInt32(MySql.sdr["id_munkakor"]),
                        id_munkakor2 = Convert.ToInt32(MySql.sdr["id_munkakor2"]),
                        id_munkakor3 = Convert.ToInt32(MySql.sdr["id_munkakor3"]),
                        vegz_terulet = MySql.sdr["vegz_terulet"].ToString(),
                        id_vegz_terulet = Convert.ToInt32(MySql.sdr["id_vegz_terulet"]),
                        nyelvtudas = MySql.sdr["nyelvtudas"].ToString(),
                        nyelvtudas2 = MySql.sdr["nyelvtudas2"].ToString(),
                        id_nyelvtudas = Convert.ToInt32(MySql.sdr["id_nyelvtudas"]),
                        id_nyelvtudas2 = Convert.ToInt32(MySql.sdr["id_nyelvtudas2"]),
                        reg_date = MySql.sdr["reg_date"].ToString(),
                        megjegyzes = MySql.sdr["megjegyzes"].ToString(),
                        folderUrl = MySql.sdr["folderUrl"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }
}
