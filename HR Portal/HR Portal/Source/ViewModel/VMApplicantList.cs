using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.ViewModel
{
    class VMApplicantList
    {
        public static List<JeloltListItems> getApplicantList(List<string> searchValue) //javított
        {
            List<JeloltListItems> list = new List<JeloltListItems>();

            string command = "SELECT " +
                "coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id GROUP BY jelolt_id),0) as interjuk_db, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor) as munkakor, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2) as munkakor2, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3) as munkakor3, " +
                "jeloltek.id,jeloltek.nev,szuldatum,reg_date,allapota,kolcsonzott,email " +
                "FROM jeloltek " +
                "LEFT JOIN megjegyzesek ON jeloltek.id = megjegyzesek.jelolt_id " +
                "LEFT JOIN munkakor on jeloltek.munkakor = munkakor.id " +
                "LEFT JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "WHERE jeloltek.id LIKE '%%'";

            if (searchValue[0] != "")
            {
                command += " AND jeloltek.nev LIKE '%" + searchValue[0] + "%' ";
            }
            if (searchValue[1] != "")
            {
                command += " AND jeloltek.lakhely LIKE '%" + searchValue[1] + "%' ";
            }
            if (searchValue[2] != "")
            {
                command += " AND jeloltek.email LIKE '%" + searchValue[2] + "%' ";
            }
            if (searchValue[3] != "")
            {
                command += " AND jeloltek.szuldatum <= " + searchValue[3] + " ";
            }
            if (searchValue[4] != "" && searchValue[4] != "0")
            {
                command += "AND jeloltek.tapasztalat_ev >= " + searchValue[4] + " ";
            }
            if (searchValue[5] != "")
            {
                command += " AND jeloltek.reg_date LIKE '%" + searchValue[5] + "%' ";
            }
            if (searchValue[6] != "" && searchValue[6] != "0")
            {
                command += " AND coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id Group by projekt_id),0) >= " + searchValue[6] + " ";
            }
            if (searchValue[7] != "")
            {
                command += " AND jeloltek.neme LIKE '%" + searchValue[7] + "%' ";
            }
            if (searchValue[8] != "")
            {
                command += " AND jeloltek.munkakor LIKE '%" + searchValue[8] + "%' ";
            }
            if (searchValue[9] != "")
            {
                command += " AND jeloltek.vegz_terulet LIKE '%" + searchValue[9] + "%' ";
            }
            if (searchValue[10] != "")
            {
                command += " AND megjegyzesek.megjegyzes LIKE '%" + searchValue[10] + "%' ";
            }
            if (searchValue[11] == "1")
            {
                command += "  AND projekt_jelolt_kapcs.id IS NULL ";
            }
            command += " GROUP BY jeloltek.email ";
            switch (searchValue[12])
            {
                case "1":
                    command += " ORDER BY jeloltek.id" + searchValue[13];
                    break;
                case "2":
                    command += " ORDER BY jeloltek.nev" + searchValue[13];
                    break;
                case "3":
                    command += " ORDER BY jeloltek.munkakor" + searchValue[13];
                    break;
                case "4":
                    command += " ORDER BY interjuk_db" + searchValue[13];
                    break;
                case "5":
                    command += " ORDER BY jeloltek.szuldatum" + searchValue[13];
                    break;
                case "6":
                    command += " ORDER BY jeloltek.reg_date" + searchValue[13];
                    break;
                default:
                    command += " ORDER BY jeloltek.reg_date DESC";
                    break;
            }
            command += " LIMIT 25";

            
            if (MySql.open() == true)
            {
                MySqlDataReader sdr;
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                sdr = MySql.cmd.ExecuteReader();
                while (sdr.Read())
                {
                    string allapot_megnev = "Beérkezett", kolcsonzott = "";
                    int allapot = 0;
                    try
                    {
                        allapot = Convert.ToInt32(sdr["allapota"]);
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

                    if (Convert.ToInt32(sdr["kolcsonzott"]) == 1)
                        kolcsonzott = "Kölcsönzött";
                    list.Add(new JeloltListItems
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        nev = sdr["nev"].ToString(),
                        munkakor = sdr["munkakor"].ToString(),
                        munkakor2 = sdr["munkakor2"].ToString(),
                        munkakor3 = sdr["munkakor3"].ToString(),
                        email = sdr["email"].ToString(),
                        szuldatum = Convert.ToInt32(sdr["szuldatum"]),
                        interjuk_db = Convert.ToInt32(sdr["interjuk_db"]),
                        allapota = allapot,
                        kolcsonzott = kolcsonzott,
                        allapot_megnevezes = allapot_megnev,
                        reg_datum = sdr["reg_date"].ToString(),
                    });
                }
                sdr.Close();
            }
            MySql.close();

            return list;
        }

    }
}
