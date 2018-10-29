﻿using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;
using System;
using System.Collections.Generic;

namespace HR_Portal.Source.ViewModel
{
    interface Applicant
    {
        List<ModelApplicantList> GetApplicantList(List<string> searchValue = null);

        List<ModelFullApplicant> GetFullApplicant();

        List<ModelSmallProject> Data_ProjectList();

        List<ModelSmallProject> Data_PorjectListSmall();

        void Insert(List<ModelFullApplicant> items);

        void Update(List<ModelFullApplicant> items);

        void DeleteApplicant(int id);

        void DeleteProject(int id);

        void AddToProject(int jelolt_index, int projekt_index);
    }
    class ApplicantImplementation : Applicant
    {
        public List<ModelApplicantList> GetApplicantList(List<string> searchValue = null)
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

            List<ModelApplicantList> list = ModelApplicantList.GetModelApplicantList(command);

            MySql.Close();

            return list;
        }

        public List<ModelFullApplicant> GetFullApplicant()
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

            List<ModelFullApplicant> list = ModelFullApplicant.GetModelFullApplicant(command);

            MySql.Close();

            return list;
        }

        public void DeleteApplicant(int id)   //javított használja: applicantlist
        {
            string command = "DELETE FROM jeloltek WHERE jeloltek.id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM kepessegek WHERE kepessegek.jelolt_id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = " + id + ";";
            MySql.Update(command);
            command = "DELETE FROM megjegyzesek WHERE megjegyzesek.jelolt_id = " + id + ";";
            MySql.Update(command);
            MySql.Close();
        }

        public void Insert(List<ModelFullApplicant> items)  //javított
        {
            string command = "INSERT INTO jeloltek (`id`, `nev`, `email`, `telefon`, `lakhely`, `ertesult`, `szuldatum`, neme, `tapasztalat_ev`, `munkakor`, `munkakor2`, `munkakor3`, `vegz_terulet`, `nyelvtudas`,`nyelvtudas2`, `reg_date`) " +
                "VALUES(NULL, '" + items[0].nev + "',  '" + items[0].email + "', '" + items[0].telefon + "', '" + items[0].lakhely + "', " + items[0].ertesult + ", " + items[0].szuldatum + ", " + items[0].neme + "," + items[0].tapasztalat_ev + "," + items[0].munkakor + "," + items[0].munkakor2 + "," + items[0].munkakor3 + "," + items[0].vegz_terulet + "," + items[0].nyelvtudas + "," + items[0].nyelvtudas2 + ",'" + items[0].reg_date + "');";
            MySql.Update(command);
            command = "SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "'";
            MySql.Close();
            Session.ApplicantID = Convert.ToInt16(MySql.UniqueList(command, "jeloltek", 1)[0]);
            MySql.Close();
        }

        public void Update(List<ModelFullApplicant> items)  //javított
        {
            string query = "UPDATE jeloltek SET " +
                " `nev` = '" + items[0].nev + "'" +
                ", `email` = '" + items[0].email + "'" +
                ", `telefon` = '" + items[0].telefon + "'" +
                ", `lakhely` = '" + items[0].lakhely + "'" +
                ", `ertesult` = " + items[0].ertesult + "" +
                ", `szuldatum` = '" + items[0].szuldatum + "'" +
                ", `neme` = " + items[0].neme + "" +
                ", `tapasztalat_ev` = " + items[0].tapasztalat_ev + "" +
                ", `munkakor` = " + items[0].munkakor + "" +
                ", `munkakor2` = " + items[0].munkakor2 + "" +
                ", `munkakor3` = " + items[0].munkakor3 + "" +
                ", `vegz_terulet` = " + items[0].vegz_terulet + "" +
                ", `nyelvtudas` = " + items[0].nyelvtudas + "" +
                ",`nyelvtudas2` = " + items[0].nyelvtudas2 + "" +
                ", `reg_date`  = '" + items[0].reg_date + "'" +
                "WHERE jeloltek.id = " + Session.ApplicantID + "";
            MySql.Update(query);
            int appID = Convert.ToInt16(MySql.UniqueList("SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "' AND jeloltek.lakhely = '" + items[0].lakhely + "'", "jeloltek", 1)[0]);
            Session.ApplicantID = appID;
            MySql.Close();
        }

        public List<ModelSmallProject> Data_ProjectList() //javított
        {
            string command = "SELECT projektek.id, megnevezes_projekt FROM projektek " +
                "INNER JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id " +
                "INNER JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "WHERE jeloltek.id = " + Session.ApplicantID + " " +
                "GROUP BY projektek.id";
            List<ModelSmallProject> list = ModelSmallProject.GetModelSmallProject(command);
            MySql.Close();
            return list;
        }

        public List<ModelSmallProject> Data_PorjectListSmall()  //javított
        {
            string command = "SELECT projektek.id, megnevezes_projekt FROM projektek WHERE statusz = 1";
            List<ModelSmallProject> list = ModelSmallProject.GetModelSmallProject(command);
            MySql.Close();
            return list;
        }

        public void DeleteProject(int id)  //javított
        {
            string command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_id = " + id + " AND jelolt_id = " + Session.ApplicantID + ";";
            MySql.Update(command);
            MySql.Close();
        }
        
        public void AddToProject(int jelolt_index, int projekt_index)
        {
            string command = "SELECT * FROM projekt_jelolt_kapcs WHERE jelolt_id = " + jelolt_index + " AND projekt_id = "+projekt_index+"";
            if (!MySql.IsExists(command))
            {
                MySql.Close();
                DateTime dateTime = DateTime.Now;
                command = "INSERT INTO projekt_jelolt_kapcs (id, projekt_id, jelolt_id, hr_id, datum) VALUES (NULL, " + projekt_index + ", " + jelolt_index + ", " + Session.UserData[0].id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "' );";
                MySql.Update(command);
            }
            MySql.Close();
        }
    }
}