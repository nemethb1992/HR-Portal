using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Project;
using System;
using System.Collections.Generic;

namespace HR_Portal.Source.ViewModel
{
    public class Project
    {
        public ModelFullProject data;
        public Project(int projectId = 0)
        {
            this.data = GetFullProject(projectId)[0];
        }

        public void RefreshData()
        {
            this.data = GetFullProject(data.id)[0];
        }

        public static List<ModelProjectList> GetProjectList(ModelProjectSearchBar value)
        {
            List<ModelProjectList> list = new List<ModelProjectList>();

            string command = "SELECT coalesce((SELECT count(jelolt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id GROUP BY jeloltek.id LIMIT 1),0) as jeloltek_db, coalesce((SELECT count(jelolt_id) FROM interview WHERE projekt_id = projektek.id LIMIT 1),0) as interjuk_db, projektek.id, projektek.publikalt, megnevezes_projekt, megnevezes_munka, fel_datum, projektek.statusz FROM projektek LEFT JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id LEFT JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id LEFT JOIN munkakor ON munkakor.id = projektek.munkakor LEFT JOIN pc ON pc.id = projektek.pc LEFT JOIN megjegyzesek ON projektek.id = megjegyzesek.projekt_id " +
            " WHERE projektek.statusz=" + Session.ProjectStatusz;
            if (value.projektnev != "")
            {
                command += " AND projektek.megnevezes_projekt LIKE '%" + value.projektnev + "%' ";
            }
            if (value.jeloltszam != "0")
            {
                command += " AND coalesce((SELECT count(projekt_id)  FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id Group by projekt_id LIMIT 1),0) >=" + value.jeloltszam + " ";
            }
            if (value.publikalva != "")
            {
                command += " AND projektek.fel_datum LIKE '%" + value + "%' ";
            }
            if (value.interjuk != "0")
            {
                command += " AND coalesce((SELECT count(jelolt_id) FROM interview WHERE projekt_id = projektek.id Group by jelolt_id LIMIT 1),0) >=" + value.interjuk + " ";
            }
            if (value.pc != "")
            {
                command += " AND pc.megnevezes_pc LIKE '%" + value.pc + "%' ";
            }
            if (value.nyelvkStr != "" && value.nyelvkStr != "1")
            {
                command += " AND projektek.nyelvtudas LIKE '%" + value.nyelvkStr + "%' ";
            }
            if (value.vegzettsegStr != "" && value.vegzettsegStr != "1")
            {
                command += " AND projektek.vegzettseg LIKE '%" + value.vegzettsegStr + "%' ";
            }
            if (value.cimke != "")
            {
                command += " AND megjegyzesek.megjegyzes LIKE '%" + value.cimke + "%' ";
            }
            if (value.jeloltnev != "")
            {
                command += " AND jeloltek.nev LIKE '%" + value.jeloltnev + "%' ";
            }
            if (value.publikalt != "")
            {
                command += " AND projektek.publikalt LIKE '%" + value.publikalt + "%' ";
            }
            command += " GROUP BY projektek.id ";
            switch (value.HeaderSelected)
            {
                case "1":
                    command += " ORDER BY projektek.id" + value.sorrend;
                    break;
                case "2":
                    command += " ORDER BY projektek.megnevezes_projekt" + value.sorrend;
                    break;
                case "3":
                    command += " ORDER BY projektek.munkakor" + value.sorrend;
                    break;
                case "4":
                    command += " ORDER BY jeloltek_db" + value.sorrend;
                    break;
                case "5":
                    command += " ORDER BY projektek.fel_datum" + value.sorrend;
                    break;
                default:
                    command += " ORDER BY projektek.fel_datum DESC";
                    break;
            }

            return ModelProjectList.GetModelProjectList(command);
        }



        public static List<ModelFullProject> GetFullProject(int id = 0)
        {
            string command = "SELECT (SELECT count(projekt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = "+Session.ProjektID+") as jeloltek_db, " +
                "projektek.id, projektek.hr_id, megnevezes_projekt, megnevezes_vegzettseg, megnevezes_nyelv,megnevezes_munka,megnevezes_pc,name,fel_datum,le_datum,pc,vegzettseg,tapasztalat_ev,allapot,nyelvtudas,munkakor,szuldatum,ber,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5,feladatok,elvarasok,kinalunk, elonyok, publikalt  " +
                "FROM projektek " +
                "LEFT JOIN munkakor on munkakor.id = projektek.munkakor " +
                "LEFT JOIN nyelv ON nyelv.id = projektek.nyelvtudas " +
                "LEFT JOIN vegzettsegek ON vegzettsegek.id = projektek.vegzettseg " +
                "LEFT JOIN users ON users.id = projektek.hr_id " +
                "LEFT JOIN pc ON pc.id = projektek.pc " +
                "LEFT JOIN statusz ON projektek.statusz = statusz.id " +
                "WHERE projektek.id = " + (!id.Equals(0) ? id : Session.ProjektID) + " GROUP BY projektek.id";

            List<ModelFullProject> list = ModelFullProject.GetModelFullProject(command);
            

            return list;
        }

        public static void Delete(int id) // javított
        {
            MySqlDB mySql = new MySqlDB();
            string command;
            command = "DELETE FROM projektek WHERE projektek.id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.projekt_id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM projekt_hr_kapcs WHERE projekt_hr_kapcs.projekt_id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM projekt_ertesitendok_kapcs WHERE projekt_ertesitendok_kapcs.projekt_id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM megjegyzesek WHERE megjegyzesek.projekt_id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM interview WHERE interview.projekt_id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.projekt_id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM interview WHERE interview.projekt_id=" + id + " AND hr_id=" + Session.UserData.id + "";
            mySql.Execute(command);
            mySql.Close();
        }

        public static void Insert(ModelInsertProject data) // javított newprojectpanel
        {
            MySqlDB mySql = new MySqlDB();
            string command = "INSERT INTO projektek (`id`, `hr_id`, `megnevezes_projekt`, `pc`, `vegzettseg`, `tapasztalat_ev`, `statusz`, `fel_datum`, `le_datum`, `nyelvtudas`, `munkakor`, `szuldatum`, `ber`,  `kepesseg1`, `kepesseg2`, `kepesseg3`, `kepesseg4`, `kepesseg5`, `feladatok`, `elvarasok`, `kinalunk`)" +
                " VALUES (NULL, " + data.hr_id + ", '" + data.megnevezes_projekt + "'," + data.pc + "," + data.vegzettseg + "," + data.tapasztalat_ev + "," + data.statusz + ",'" + data.fel_datum + "','" + data.le_datum + "'," + data.nyelvtudas + "," + data.munkakor + "," + data.szuldatum + "," + data.ber + "," + data.kepesseg1 + "," + data.kepesseg2 + "," + data.kepesseg3 + "," + data.kepesseg4 + "," + data.kepesseg5 + ",'" + data.feladatok + "','" + data.elvarasok + "','" + data.kinalunk + "');";
            mySql.Execute(command);
            int proID = Convert.ToInt16(mySql.UniqueList("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + data.megnevezes_projekt + "' AND projektek.pc = " + data.pc + " AND projektek.munkakor = '" + data.munkakor + "'", "projektek", 1)[0]);
            Session.ProjektID = proID;
            command = "INSERT INTO`projekt_ertesitendok_kapcs` (projekt_id,ertesitendok_id) VALUES("+ proID + ","+Session.UserData.id+")";
            mySql.Execute(command);
            mySql.Close();
        }

        public static void Update(ModelInsertProject data) // javított newprojectpanel
        {
            MySqlDB mySql = new MySqlDB();
            string command = "UPDATE projektek SET " +
                " `hr_id` =  " + data.hr_id + ", " +
                "`megnevezes_projekt` =  '" + data.megnevezes_projekt + "', " +
                " `pc` =  " + data.pc + ", " +
                "`vegzettseg` =  " + data.vegzettseg + ", " +
                "`tapasztalat_ev` =  " + data.tapasztalat_ev + ", " +
                "`statusz` =  " + data.statusz + ", " +
                "`nyelvtudas` =  " + data.nyelvtudas + ", " +
                "`munkakor` =  " + data.munkakor + ", " +
                "`szuldatum` =  " + data.szuldatum + ", " +
                "`ber` =  " + data.ber + ", " +
                "`kepesseg1` =  " + data.kepesseg1 + ", " +
                "`kepesseg2` =  " + data.kepesseg2 + ", " +
                "`kepesseg3` =  " + data.kepesseg3 + ", " +
                "`kepesseg4` =  " + data.kepesseg4 + ", " +
                "`kepesseg5` =  " + data.kepesseg5 + " WHERE id = " + Session.ProjektID + "";
            mySql.Execute(command);
            int proID = Convert.ToInt16(mySql.UniqueList("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + data.megnevezes_projekt + "' AND projektek.pc = " + data.pc + " AND projektek.munkakor = '" + data.munkakor + "'", "projektek", 1)[0]);
            Session.ProjektID = proID;
            mySql.Close();
        }

        public static List<ModelKoltsegek> Data_ProjectCost()  // javított
        {
            string command = "SELECT * FROM projekt_koltsegek WHERE projekt_id = " + Session.ProjektID + "";
            List<ModelKoltsegek> list = ModelKoltsegek.GetModelKoltsegek(command);
            return list;
        }

        public void projectDescriptionUpdate(string type, string content) // javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "";
            switch (type)
            {
                case "feladatok":
                    command = "UPDATE projektek SET feladatok='" + content + "' WHERE projektek.id = " + Session.ProjektID + "";
                    break;
                case "elvarasok":
                    command = "UPDATE projektek SET elvarasok='" + content + "' WHERE projektek.id = " + Session.ProjektID + "";
                    break;
                case "kinalunk":
                    command = "UPDATE projektek SET kinalunk='" + content + "' WHERE projektek.id = " + Session.ProjektID + "";
                    break;
                case "elonyok":
                    command = "UPDATE projektek SET elonyok='" + content + "' WHERE projektek.id = " + Session.ProjektID + "";
                    break;
                default:
                    break;
            }
            mySql.Execute(command);
            mySql.Close();
        }

        public static void ProjectStatusChange(int stat) // javított
        {
            Session.ProjectStatusz = 0;
            Session.ProjectStatusz = stat;
        }


        public void projectCostInsert(string megnevezes, string osszeg)  // javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "INSERT INTO `projekt_koltsegek` (id, projekt_id, koltseg_megnevezes, osszeg) VALUES (null, " + Session.ProjektID + ", '" + megnevezes + "', " + osszeg + ");";
            mySql.Execute(command);
            mySql.Close();
        }

        public void projectCostDelete(int id)  // javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.id = " + id + "";
            mySql.Execute(command);
            mySql.Close();
        }

        public void addErtesitendokInsert(int index)
        {
            MySqlDB mySql = new MySqlDB();
            if (!mySql.IsExists("SELECT * FROM projekt_ertesitendok_kapcs WHERE ertesitendok_id = " + index + " AND projekt_id = "+data.id))
            {
                string command = "INSERT INTO projekt_ertesitendok_kapcs (id, projekt_id, ertesitendok_id) VALUES (NULL, " + Session.ProjektID + ", " + index + " );";
                mySql.Execute(command);
            }
            mySql.Close();
        }

        public void jeloltKapcsDelete(int id)
        {
            MySqlDB mySql = new MySqlDB();
            string command;
            command = "DELETE FROM projekt_jelolt_kapcs WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            mySql.Execute(command);
            command = "DELETE FROM interview WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            mySql.Execute(command);
            mySql.Close();
        }

        public void jeloltKapcsUpdate(int id, int allapota)
        {
            MySqlDB mySql = new MySqlDB();
            string command = "UPDATE projekt_jelolt_kapcs SET allapota = " + allapota + " WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            mySql.Execute(command);
            mySql.Close();
        }

        public void ertesitendokKapcsDelete(int id)
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM projekt_ertesitendok_kapcs WHERE ertesitendok_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            mySql.Execute(command);
            mySql.Close();
        }

        public void publishProject(int stat)
        {
            MySqlDB mySql = new MySqlDB();
            string command = "UPDATE projektek SET publikalt= " + stat + " WHERE projektek.id = " + Session.ProjektID + ";";
            mySql.Execute(command);
            mySql.Close();
        }

        public void projectArchiver(int id, int statusz) // javított
        {
            MySqlDB mySql = new MySqlDB();
            if (statusz == 0)
            {
                statusz = 1;
            }
            else
            {
                statusz = 0;
            }
            string command = "UPDATE projektek SET statusz=" + statusz + " WHERE projektek.id = " + id + ";";
            mySql.Execute(command);
            mySql.Close();
        }
    }
}
