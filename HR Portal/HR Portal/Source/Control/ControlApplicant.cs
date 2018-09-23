using HR_Portal.Source;
using System;
using System.Collections.Generic;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;

namespace HR_Portal.Control
{
    class ControlApplicant
    {
        Source.MySql mySql = new Source.MySql();



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
            List<ModelNyelv> list = ModelNyelv.getModelNyelv(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelErtesulesek> Data_Ertesulesek() //javított
        {
            string command = "SELECT * FROM ertesulesek";
            List<ModelErtesulesek> list = ModelErtesulesek.getModelErtesulesek(command);
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
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE jelolt_id=" + Session.ApplicantID;
            List<ModelComment> list = mySql.Megjegyzesek_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelSmallProject> Data_ProjectList() //javított
        {
            string command = "SELECT projektek.id, megnevezes_projekt FROM projektek " +
                "INNER JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id " +
                "INNER JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "WHERE jeloltek.id = "+ Session.ApplicantID +" " +
                "GROUP BY projektek.id";
            List<ModelSmallProject> list = ModelSmallProject.getModelSmallProject(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelSmallProject> Data_PorjectListSmall()  //javított
        {
            string command = "SELECT projektek.id, megnevezes_projekt FROM projektek WHERE statusz = 1";
            List<ModelSmallProject> list = ModelSmallProject.getModelSmallProject(command);
            Source.MySql.close();
            return list;
        }
        public void applicalntProjectListDelete(int id)  //javított
        {
            string command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_id = " + id + " AND jelolt_id = " + Session.ApplicantID + ";";
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

        public void applicantInsert(List<ModelFullApplicant> items)  //javított
        {
            string command = "INSERT INTO jeloltek (`id`, `nev`, `email`, `telefon`, `lakhely`, `ertesult`, `szuldatum`, neme, `tapasztalat_ev`, `munkakor`, `munkakor2`, `munkakor3`, `vegz_terulet`, `nyelvtudas`,`nyelvtudas2`, `reg_date`) " +
                "VALUES(NULL, '" + items[0].nev + "',  '" + items[0].email + "', '" + items[0].telefon + "', '" + items[0].lakhely + "', " + items[0].ertesult + ", " + items[0].szuldatum + ", " + items[0].neme + "," + items[0].tapasztalat_ev + "," + items[0].munkakor + "," + items[0].munkakor2 + "," + items[0].munkakor3 + "," + items[0].vegz_terulet + "," + items[0].nyelvtudas + "," + items[0].nyelvtudas2 + ",'" + items[0].reg_date +"');";
            mySql.update(command);
            command = "SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "'";
            Source.MySql.close();
            Session.ApplicantID = Convert.ToInt16(mySql.listQuery(command, "jeloltek", 1)[0]);
            Source.MySql.close();
        }

        public void applicantUpdate(List<ModelFullApplicant> items)  //javított
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
                "WHERE jeloltek.id = " + Session.ApplicantID +"";
            mySql.update(query);
            int appID = Convert.ToInt16(mySql.listQuery("SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "' AND jeloltek.lakhely = '" + items[0].lakhely + "'", "jeloltek", 1)[0]);
            Session.ApplicantID = appID;
            Source.MySql.close();
        }
    }
}