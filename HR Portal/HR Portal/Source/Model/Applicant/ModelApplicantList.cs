using HR_Portal.Source.Model.Project;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Applicant
{
    public class ModelApplicantList
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string munkakor { get; set; }
        public string munkakor2 { get; set; }
        public string munkakor3 { get; set; }
        public int szuldatum { get; set; }
        public string email { get; set; }
        public int interjuk_db { get; set; }
        public int project_db { get; set; }
        public int allapota { get; set; }
        public string kolcsonzott { get; set; }
        public string allapot_megnevezes { get; set; }
        public string reg_datum { get; set; }
        public string megjegyzes { get; set; }
        public bool checkbox { get; set; }
        public bool frissValue { get; set; }
        public string friss { get; set; }
        public bool allasbanValue { get; set; }
        public string allasban { get; set; }
        public string profil_icon { get; set; }
        public bool szabad { get; set; }
        public int statusz { get; set; }

        public static List<ModelApplicantList> GetModelApplicantList(string command)
        {
            List<ModelApplicantList> list = new List<ModelApplicantList>();
            MySql mySql = new MySql();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();

                while (mySql.sdr.Read())
                {
                    string allapot_megnev = "Beérkezett", kolcsonzott = "", frissSeged = "Transparent", allasbanSeged = "Hidden";
                    int allapot = 0;
                    try
                    {
                        allapot = Convert.ToInt32(mySql.sdr["allapota"]);
                    }
                    catch (Exception)
                    {
                    }

                    string imgsrc = "/Public/imgs/pm_logo_mini.png";
                    if (Convert.ToInt32(mySql.sdr["profession_type"]) == 1)
                    {
                        imgsrc = "/Public/imgs/profession-logo-mini.png";
                    }

                    switch (allapot)
                    {
                        case 1:
                            allapot_megnev = "Telefonon szűrt";
                            break;
                        case 2:
                            allapot_megnev = "Felvett";
                            break;
                        case 3:
                            allapot_megnev = "Elutasított";
                            break;
                        case 4:
                            allapot_megnev = "Projekthez kapcsolt";
                            break;

                        default:
                            allapot_megnev = "Beérkezett";
                            break;
                    }
                    if (Convert.ToBoolean(mySql.sdr["allasban"]))
                        allasbanSeged = "Visible";

                    if (Convert.ToBoolean(mySql.sdr["friss"]))
                       frissSeged = "#defee3";

                    if (Convert.ToInt32(mySql.sdr["kolcsonzott"]) == 1)
                        kolcsonzott = "Kölcsönzött";

                    list.Add(new ModelApplicantList
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        nev = mySql.sdr["nev"].ToString(),
                        munkakor = mySql.sdr["munkakor"].ToString(),
                        munkakor2 = mySql.sdr["munkakor2"].ToString(),
                        munkakor3 = mySql.sdr["munkakor3"].ToString(),
                        email = mySql.sdr["email"].ToString(),
                        szuldatum = Convert.ToInt32(mySql.sdr["szuldatum"]),
                        statusz = Convert.ToInt32(mySql.sdr["statusz"]),
                        interjuk_db = Convert.ToInt32(mySql.sdr["interjuk_db"]),
                        project_db = Convert.ToInt32(mySql.sdr["project_db"]),
                        friss = frissSeged,
                        frissValue = Convert.ToBoolean(mySql.sdr["friss"]),
                        allasban = allasbanSeged,
                        allasbanValue = Convert.ToBoolean(mySql.sdr["allasban"]),
                        allapota = allapot,
                        kolcsonzott = kolcsonzott,
                        allapot_megnevezes = allapot_megnev,
                        reg_datum = mySql.sdr["reg_date"].ToString(),
                        profil_icon = imgsrc,
                        megjegyzes = mySql.sdr["megjegyzes"].ToString()
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }


    }
}
