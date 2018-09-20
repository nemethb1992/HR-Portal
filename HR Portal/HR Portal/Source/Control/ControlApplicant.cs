﻿using HR_Portal.Source;
using System;
using System.Collections.Generic;
using HR_Portal.Source.Model;

namespace HR_Portal.Control
{
    class ControlApplicant
    {
        Session session = new Session();
        Source.MySql mySql = new Source.MySql();

        public List<JeloltListItems> applicantList(List<string> searchValue) //javított
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
            List<JeloltListItems> list = mySql.getApplicantList(command);
            Source.MySql.close();
            return list;
        }

        public List<JeloltExtendedList> Data_JeloltFull() // javítva newapplicantpanel
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
                "FROM jeloltek WHERE jeloltek.id = " + session.ApplicantID + "";
            //string query = "SELECT * FROM jeloltek INNER JOIN nyelv on jeloltek.nyelvtudas = nyelv.id INNER JOIN munkakor on jeloltek.munkakor = munkakor.id INNER JOIN ertesulesek ON jeloltek.ertesult = ertesulesek.id WHERE jeloltek.id = " + ApplicantID + "";
            List<JeloltExtendedList> list = mySql.JeloltExtended_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelMunkakor> Data_Munkakor() //javított
        {
            string command = "SELECT * FROM munkakor";
            List<ModelMunkakor> list = mySql.getMunkakorok(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelStatusz> Data_Statusz() //javított
        {
            string command = "SELECT * FROM statusz";
            List<ModelStatusz> list = mySql.Statusz_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelPc> Data_Pc() //javított
        {
            string command = "SELECT * FROM pc";
            List<ModelPc> list = mySql.getPc(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelVegzettseg> Data_Vegzettseg() //javított
        {
            string command = "SELECT * FROM vegzettsegek";
            List<ModelVegzettseg> list = mySql.Vegzettseg_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelNyelv> Data_Nyelv() //javított
        {
            string command = "SELECT * FROM nyelv";
            List<ModelNyelv> list = mySql.getNyelv(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelErtesulesek> Data_Ertesulesek() //javított
        {
            string command = "SELECT * FROM ertesulesek";
            List<ModelErtesulesek> list = mySql.Ertesulesek_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelNem> Data_Nemek() //javított
        {
            string command = "SELECT * FROM nemek";
            List<ModelNem> list = mySql.Nem_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelComment> Data_Comment() //javított
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE jelolt_id=" + session.ApplicantID;
            List<ModelComment> list = mySql.Megjegyzesek_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<SmallProjectListItems> Data_ProjectList() //javított
        {
            string query = "SELECT projektek.id, megnevezes_projekt FROM projektek " +
                "INNER JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id " +
                "INNER JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "WHERE jeloltek.id = "+ session.ApplicantID +" " +
                "GROUP BY projektek.id";
            List<SmallProjectListItems> list = mySql.Small_Projekt_MySql_listQuery(query);
            Source.MySql.close();
            return list;
        }

        public List<SmallProjectListItems> Data_PorjectListSmall()  //javított
        {
            string command = "SELECT projektek.id, megnevezes_projekt FROM projektek WHERE statusz = 1";
            List<SmallProjectListItems> list = mySql.Small_Projekt_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }
        public void applicalntProjectListDelete(int id)  //javított
        {
            string command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_id = " + id + " AND jelolt_id = " + session.ApplicantID + ";";
            mySql.update(command);
            Source.MySql.close();
        }

        public void applicantFullDelete(int id)   //javított használja: applicantlist
        {
            string command = "DELETE FROM jeloltek WHERE jeloltek.id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM kepessegek WHERE kepessegek.jelolt_id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM megjegyzesek WHERE megjegyzesek.jelolt_id = " + id + ";";
            mySql.update(command);
            Source.MySql.close();
        }

        public void applicantInsert(List<JeloltExtendedList> items)  //javított
        {
            string command = "INSERT INTO jeloltek (`id`, `nev`, `email`, `telefon`, `lakhely`, `ertesult`, `szuldatum`, neme, `tapasztalat_ev`, `munkakor`, `munkakor2`, `munkakor3`, `vegz_terulet`, `nyelvtudas`,`nyelvtudas2`, `reg_date`) " +
                "VALUES(NULL, '" + items[0].nev + "',  '" + items[0].email + "', '" + items[0].telefon + "', '" + items[0].lakhely + "', " + items[0].ertesult + ", " + items[0].szuldatum + ", " + items[0].neme + "," + items[0].tapasztalat_ev + "," + items[0].munkakor + "," + items[0].munkakor2 + "," + items[0].munkakor3 + "," + items[0].vegz_terulet + "," + items[0].nyelvtudas + "," + items[0].nyelvtudas2 + ",'" + items[0].reg_date +"');";
            mySql.update(command);
            command = "SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "'";
            Source.MySql.close();
            session.ApplicantID = Convert.ToInt16(mySql.listQuery(command, "jeloltek", 1)[0]);
            Source.MySql.close();
        }

        public void applicantUpdate(List<JeloltExtendedList> items)  //javított
        {
            string query = "UPDATE jeloltek SET "+
                " `nev` = '"+items[0].nev+"'" +
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
                "WHERE jeloltek.id = " + session.ApplicantID +"";
            mySql.update(query);
            int appID = Convert.ToInt16(mySql.listQuery("SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "' AND jeloltek.lakhely = '" + items[0].lakhely + "'", "jeloltek", 1)[0]);
            session.ApplicantID = appID;
            Source.MySql.close();
        }

        //public List<csatolmany_struct> CsatolmanyDataSource()
        //{
        //    string query = "SELECT jeloltek.id, jeloltek.nev, tipus, fajlnev,kapcs_id, kiterjesztes FROM csatolmanyok INNER JOIN jeloltek ON csatolmanyok.kapcs_id = jeloltek.id WHERE jeloltek.id = " + ApplicantID + " AND tipus=0";
        //    return dbE.Csatolmany_MySql_listQuery(query);
        //}
        //public bool CvDownload(string nev, string fajlnev, string kiterjesztes)
        //{
        //    string url = "applicant_cv";
        //    return ftpE.ftpFileDownload(nev, url, fajlnev, kiterjesztes);
        //}
        //public bool CvUpload(string path)
        //{
        //    bool respond = false;
        //    string nev = dbE.MysqlReaderExecute_List("Select nev from jeloltek where id=" + ApplicantID + "", "jeloltek", 1)[0];
        //    string url = "applicant_cv";

        //    List<ftp_feltoltes> li = ftpE.ftpFileUpload(path, url, ApplicantID.ToString(), nev);
        //    try
        //    {
        //        if (li[0].allapot)
        //        {
        //            string fajlnev = "HRcloud_" + nev.Replace(' ', '_') + "_cv." + li[0].kiterjesztes;
        //            string query = "INSERT INTO csatolmanyok (kapcs_id,tipus,fajlnev,kiterjesztes) VALUES (" + ApplicantID + ",0,'" + fajlnev + "','" + li[0].kiterjesztes + "')";
        //            dbE.update(query);
        //            respond = true;
        //        }
        //    }
        //    catch (Exception)  { }
        //    return respond;
        //}
    }
}