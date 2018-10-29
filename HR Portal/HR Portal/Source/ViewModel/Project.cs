﻿using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Project;
using System;
using System.Collections.Generic;

namespace HR_Portal.Source.ViewModel
{
    class Project
    {
        public static List<ModelProjectList> GetProjectList(List<string> searchValue)
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

            return ModelProjectList.GetModelProjectList(command);
        }

        public static List<ModelFullProject> GetFullProject()
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
                "WHERE projektek.id = " + Session.ProjektID + " GROUP BY projektek.id";

            List<ModelFullProject> list = ModelFullProject.GetModelFullProject(command);

            MySql.Close();

            return list;
        }

        public static void Delete(int id) // javított
        {
            string command;
            command = "DELETE FROM projektek WHERE projektek.id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.projekt_id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM projekt_hr_kapcs WHERE projekt_hr_kapcs.projekt_id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM projekt_ertesitendok_kapcs WHERE projekt_ertesitendok_kapcs.projekt_id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM megjegyzesek WHERE megjegyzesek.projekt_id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.projekt_id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.projekt_id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.projekt_id=" + id + " AND hr_id=" + Session.UserData[0].id + "";
            MySql.Update(command);
            MySql.Close();
        }

        public static void Insert(List<ModelInsertProject> items) // javított newprojectpanel
        {
            string command = "INSERT INTO projektek (`id`, `hr_id`, `megnevezes_projekt`, `pc`, `vegzettseg`, `tapasztalat_ev`, `statusz`, `fel_datum`, `le_datum`, `nyelvtudas`, `munkakor`, `szuldatum`, `ber`,  `kepesseg1`, `kepesseg2`, `kepesseg3`, `kepesseg4`, `kepesseg5`, `feladatok`, `elvarasok`, `kinalunk`)" +
                " VALUES (NULL, " + items[0].hr_id + ", '" + items[0].megnevezes_projekt + "'," + items[0].pc + "," + items[0].vegzettseg + "," + items[0].tapasztalat_ev + "," + items[0].statusz + ",'" + items[0].fel_datum + "','" + items[0].le_datum + "'," + items[0].nyelvtudas + "," + items[0].munkakor + "," + items[0].szuldatum + "," + items[0].ber + "," + items[0].kepesseg1 + "," + items[0].kepesseg2 + "," + items[0].kepesseg3 + "," + items[0].kepesseg4 + "," + items[0].kepesseg5 + ",'" + items[0].feladatok + "','" + items[0].elvarasok + "','" + items[0].kinalunk + "');";
            MySql.Update(command);
            int proID = Convert.ToInt16(MySql.UniqueList("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + items[0].megnevezes_projekt + "' AND projektek.pc = " + items[0].pc + " AND projektek.munkakor = '" + items[0].munkakor + "'", "projektek", 1)[0]);
            Session.ProjektID = proID;
            MySql.Close();
        }

        public static void Update(List<ModelInsertProject> items) // javított newprojectpanel
        {
            string command = "UPDATE projektek SET " +
                " `hr_id` =  " + items[0].hr_id + ", " +
                "`megnevezes_projekt` =  '" + items[0].megnevezes_projekt + "', " +
                " `pc` =  " + items[0].pc + ", " +
                "`vegzettseg` =  " + items[0].vegzettseg + ", " +
                "`tapasztalat_ev` =  " + items[0].tapasztalat_ev + ", " +
                "`statusz` =  " + items[0].statusz + ", " +
                "`nyelvtudas` =  " + items[0].nyelvtudas + ", " +
                "`munkakor` =  " + items[0].munkakor + ", " +
                "`szuldatum` =  " + items[0].szuldatum + ", " +
                "`ber` =  " + items[0].ber + ", " +
                "`kepesseg1` =  " + items[0].kepesseg1 + ", " +
                "`kepesseg2` =  " + items[0].kepesseg2 + ", " +
                "`kepesseg3` =  " + items[0].kepesseg3 + ", " +
                "`kepesseg4` =  " + items[0].kepesseg4 + ", " +
                "`kepesseg5` =  " + items[0].kepesseg5 + " WHERE id = " + Session.ProjektID + "";
            MySql.Update(command);
            int proID = Convert.ToInt16(MySql.UniqueList("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + items[0].megnevezes_projekt + "' AND projektek.pc = " + items[0].pc + " AND projektek.munkakor = '" + items[0].munkakor + "'", "projektek", 1)[0]);
            Session.ProjektID = proID;
            MySql.Close();
        }

        public static List<ModelKoltsegek> Data_ProjectCost()  // javított
        {
            string command = "SELECT * FROM projekt_koltsegek WHERE projekt_id = " + Session.ProjektID + "";
            List<ModelKoltsegek> list = ModelKoltsegek.GetModelKoltsegek(command);
            MySql.Close();
            return list;
        }
    }
}