using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Applicant
{
    class ModelApplicantList
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string munkakor { get; set; }
        public string munkakor2 { get; set; }
        public string munkakor3 { get; set; }
        public int szuldatum { get; set; }
        public string email { get; set; }
        public int interjuk_db { get; set; }
        public int allapota { get; set; }
        public string kolcsonzott { get; set; }
        public string allapot_megnevezes { get; set; }
        public string reg_datum { get; set; }
        public bool checkbox { get; set; }
        public bool frissValue { get; set; }
        public string friss { get; set; }
        public bool allasbanValue { get; set; }
        public string allasban { get; set; }
        public bool szabad { get; set; }
        public int statusz { get; set; }

        public static List<ModelApplicantList> GetModelApplicantList(string command)
        {
            List<ModelApplicantList> list = new List<ModelApplicantList>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();

                while (MySql.sdr.Read())
                {
                    string allapot_megnev = "Beérkezett", kolcsonzott = "", frissSeged = "Hidden", allasbanSeged = "Hidden";
                    int allapot = 0;
                    try
                    {
                        allapot = Convert.ToInt32(MySql.sdr["allapota"]);
                    }
                    catch (Exception)
                    {
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

                        default:
                            allapot_megnev = "Beérkezett";
                            break;
                    }
                    if (Convert.ToBoolean(MySql.sdr["allasban"]))
                        allasbanSeged = "Visible";

                    if (Convert.ToBoolean(MySql.sdr["friss"]))
                       frissSeged = "Visible";

                    if (Convert.ToInt32(MySql.sdr["kolcsonzott"]) == 1)
                        kolcsonzott = "Kölcsönzött";

                    list.Add(new ModelApplicantList
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        nev = MySql.sdr["nev"].ToString(),
                        munkakor = MySql.sdr["munkakor"].ToString(),
                        munkakor2 = MySql.sdr["munkakor2"].ToString(),
                        munkakor3 = MySql.sdr["munkakor3"].ToString(),
                        email = MySql.sdr["email"].ToString(),
                        szuldatum = Convert.ToInt32(MySql.sdr["szuldatum"]),
                        statusz = Convert.ToInt32(MySql.sdr["statusz"]),
                        interjuk_db = Convert.ToInt32(MySql.sdr["interjuk_db"]),
                        friss = frissSeged,
                        frissValue = Convert.ToBoolean(MySql.sdr["friss"]),
                        allasban = allasbanSeged,
                        allasbanValue = Convert.ToBoolean(MySql.sdr["allasban"]),
                        allapota = allapot,
                        kolcsonzott = kolcsonzott,
                        allapot_megnevezes = allapot_megnev,
                        reg_datum = MySql.sdr["reg_date"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            MySql.Close();
            return list;
        }


    }
}
