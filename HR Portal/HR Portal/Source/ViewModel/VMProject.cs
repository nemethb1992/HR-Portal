using HR_Portal.Source.Model.Project;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.ViewModel
{
    class VMProject
    {
        public static List<ModelProjectList> getProjectList(List<string> searchValue)
        {
            List<ModelProjectList> list = new List<ModelProjectList>();

            string command = "SELECT coalesce((SELECT count(jelolt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id GROUP BY jeloltek.id),0) as jeloltek_db, coalesce((SELECT count(jelolt_id) FROM interjuk_kapcs WHERE projekt_id = projektek.id),0) as interjuk_db, projektek.id, projektek.publikalt, megnevezes_projekt, megnevezes_munka, fel_datum, statusz FROM projektek LEFT JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id LEFT JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id LEFT JOIN munkakor ON munkakor.id = projektek.munkakor LEFT JOIN pc ON pc.id = projektek.pc LEFT JOIN megjegyzesek ON projektek.id = megjegyzesek.projekt_id " +
            " WHERE projektek.statusz = " + Session.ProjectStatusz;
            if (searchValue[0] != "")
            {
                command += " AND projektek.megnevezes_projekt LIKE '%" + searchValue[0] + "%' ";
            }
            if (searchValue[1] != "0")
            {
                command += " AND coalesce((SELECT count(projekt_id)  FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id Group by projekt_id),0) >=" + searchValue[1] + " ";
            }
            if (searchValue[2] != "")
            {
                command += " AND projektek.fel_datum LIKE '%" + searchValue[2] + "%' ";
            }
            if (searchValue[3] != "0")
            {
                command += " AND coalesce((SELECT count(jelolt_id) FROM interjuk_kapcs WHERE projekt_id = projektek.id Group by jelolt_id),0) >=" + searchValue[3] + " ";
            }
            if (searchValue[4] != "")
            {
                command += " AND pc.megnevezes_pc LIKE '%" + searchValue[4] + "%' ";
            }
            if (searchValue[5] != "" && searchValue[5] != "1")
            {
                command += " AND projektek.nyelvtudas LIKE '%" + searchValue[5] + "%' ";
            }
            if (searchValue[6] != "" && searchValue[6] != "1")
            {
                command += " AND projektek.vegzettseg LIKE '%" + searchValue[6] + "%' ";
            }
            if (searchValue[7] != "")
            {
                command += " AND megjegyzesek.megjegyzes LIKE '%" + searchValue[7] + "%' ";
            }
            if (searchValue[8] != "")
            {
                command += " AND jeloltek.nev LIKE '%" + searchValue[8] + "%' ";
            }
            if (searchValue[9] != "")
            {
                command += " AND projektek.publikalt LIKE '%" + searchValue[9] + "%' ";
            }
            command += " GROUP BY projektek.id ";
            switch (searchValue[10])
            {
                case "1":
                    command += " ORDER BY projektek.id" + searchValue[11];
                    break;
                case "2":
                    command += " ORDER BY projektek.megnevezes_projekt" + searchValue[11];
                    break;
                case "3":
                    command += " ORDER BY projektek.munkakor" + searchValue[11];
                    break;
                case "4":
                    command += " ORDER BY jeloltek_db" + searchValue[11];
                    break;
                case "5":
                    command += " ORDER BY projektek.fel_datum" + searchValue[11];
                    break;
                default:
                    command += " ORDER BY projektek.fel_datum DESC";
                    break;
            }

            return ModelProjectList.getModelProjectList(command);
        }

        public static List<ModelFullProject> getFullProject()
        {
            string command = "SELECT (SELECT count(projekt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id GROUP BY projekt_id) as jeloltek_db, " +
                "projektek.id, projektek.hr_id, megnevezes_projekt, megnevezes_vegzettseg, megnevezes_nyelv,megnevezes_munka,megnevezes_pc,name,fel_datum,le_datum,pc,vegzettseg,tapasztalat_ev,allapot,nyelvtudas,munkakor,szuldatum,ber,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5,feladatok,elvarasok,kinalunk, elonyok, publikalt  " +
                "FROM projektek " +
                "LEFT JOIN munkakor on munkakor.id = projektek.munkakor " +
                "LEFT JOIN nyelv ON nyelv.id = projektek.nyelvtudas " +
                "LEFT JOIN vegzettsegek ON vegzettsegek.id = projektek.vegzettseg " +
                "LEFT JOIN users ON users.id = projektek.hr_id " +
                "LEFT JOIN pc ON pc.id = projektek.pc " +
                "LEFT JOIN statusz ON projektek.statusz = statusz.id " +
                "WHERE projektek.id = " + Session.ProjektID + " GROUP BY projektek.id";

            List<ModelFullProject> list = ModelFullProject.getModelFullProject(command);

            MySql.close();

            return list;
        }
    }
}
