using HR_Portal.Source.Model.Applicant;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.ViewModel
{
    class VMApplicant
    {
        public static List<ModelApplicantList> getApplicantList(List<string> searchValue) //javított
        {
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

            List<ModelApplicantList> list = ModelApplicantList.getModelApplicantList(command);

            MySql.close();

            return list;
        }

        public static List<ModelFullApplicant> getFullApplicant()
        {
            string command = "SELECT jeloltek.id,nev,email,telefon,lakhely,pmk_ismerte,szuldatum,neme,tapasztalat_ev, reg_date,felvett,jeloltek.megjegyzes,folderUrl,hirlevel," +
                "coalesce((SELECT nem FROM nemek WHERE nemek.id = jeloltek.neme),'') AS neme," +
                "(SELECT nemek.id FROM nemek WHERE nemek.id = jeloltek.neme) AS id_neme," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor),'') AS munkakor," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),'') AS munkakor2," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),'') AS munkakor3," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor),0) AS id_munkakor," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),0) AS id_munkakor2," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),0) AS id_munkakor3," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),'') AS nyelvtudas," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),'') AS nyelvtudas2," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),0) AS id_nyelvtudas," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),0) AS id_nyelvtudas2," +
                "coalesce((SELECT ertesules_megnevezes FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),'') AS ertesules_megnevezes, " +
                "coalesce((SELECT ertesulesek.id FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),0) AS id_ertesult, " +
                "coalesce((SELECT megnevezes_vegzettseg FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),'') AS vegz_terulet, " +
                "coalesce((SELECT vegzettsegek.id FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),0) AS id_vegz_terulet " +
                "FROM jeloltek WHERE jeloltek.id = " + Session.ApplicantID + "";

            List<ModelFullApplicant> list = ModelFullApplicant.getModelFullApplicant(command);

            MySql.close();

            return list;
        }

    }
}
