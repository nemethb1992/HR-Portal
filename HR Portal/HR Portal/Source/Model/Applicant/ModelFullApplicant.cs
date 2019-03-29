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
        public int profession_type { get; set; }
        public string reg_date { get; set; }
        public string megjegyzes { get; set; }
        public string folderUrl { get; set; }
        public int statusz { get; set; }

        public static List<ModelFullApplicant> GetModelFullApplicant(string command)
        {
            List<ModelFullApplicant> list = new List<ModelFullApplicant>();
            MySql mySql = new MySql();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelFullApplicant
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        nev = mySql.sdr["nev"].ToString(),
                        email = mySql.sdr["email"].ToString(),
                        telefon = mySql.sdr["telefon"].ToString(),
                        lakhely = mySql.sdr["lakhely"].ToString(),
                        ertesult = mySql.sdr["ertesules_megnevezes"].ToString(),
                        id_ertesult = Convert.ToInt32(mySql.sdr["id_ertesult"]),
                        szuldatum = Convert.ToInt32(mySql.sdr["szuldatum"]),
                        neme = mySql.sdr["neme"].ToString(),
                        id_neme = Convert.ToInt32(mySql.sdr["id_neme"]),
                        tapasztalat_ev = Convert.ToInt32(mySql.sdr["tapasztalat_ev"]),
                        munkakor = mySql.sdr["munkakor"].ToString(),
                        munkakor2 = mySql.sdr["munkakor2"].ToString(),
                        munkakor3 = mySql.sdr["munkakor3"].ToString(),
                        id_munkakor = Convert.ToInt32(mySql.sdr["id_munkakor"]),
                        id_munkakor2 = Convert.ToInt32(mySql.sdr["id_munkakor2"]),
                        id_munkakor3 = Convert.ToInt32(mySql.sdr["id_munkakor3"]),
                        vegz_terulet = mySql.sdr["vegz_terulet"].ToString(),
                        id_vegz_terulet = Convert.ToInt32(mySql.sdr["id_vegz_terulet"]),
                        nyelvtudas = mySql.sdr["nyelvtudas"].ToString(),
                        nyelvtudas2 = mySql.sdr["nyelvtudas2"].ToString(),
                        id_nyelvtudas = Convert.ToInt32(mySql.sdr["id_nyelvtudas"]),
                        id_nyelvtudas2 = Convert.ToInt32(mySql.sdr["id_nyelvtudas2"]),
                        profession_type = Convert.ToInt32(mySql.sdr["profession_type"]),
                        reg_date = mySql.sdr["reg_date"].ToString(),
                        megjegyzes = mySql.sdr["megjegyzes"].ToString(),
                        folderUrl = mySql.sdr["folderUrl"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}
