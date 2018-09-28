using System;
using System.Collections.Generic;
using HR_Portal.Source.Model.Project;

namespace HR_Portal.Source
{
    class ControlApplicantProject
    {
        ControlApplicant aControl = new ControlApplicant();
        ControlProject pControl = new ControlProject();
        //Source.MySql mySql = new Source.MySql();

        public void telephoneFilterInsert(int ismerte,int muszakok,string utazas) //javított
        {
            string command = "UPDATE projekt_jelolt_kapcs SET allapota = 1 WHERE projekt_id = " + Session.ProjektID + " AND jelolt_id = " + Session.ApplicantID + "";
            MySql.Update(command);
            command = "UPDATE jeloltek SET pmk_ismerte = "+ismerte+"  WHERE id = " + Session.ApplicantID + "";
            MySql.Update(command);
            command = "INSERT INTO jelolt_statisztika (id, jelolt_id, utazas, muszakok) VALUES(null, "+ Session.ApplicantID+", '"+utazas+"', "+muszakok+")";
            MySql.Update(command);
            MySql.Close();
        }
        
        public List<ModelInterview> Data_Interview() //javított
        {
            string command = "SELECT interjuk_kapcs.id,megnevezes_projekt,jeloltek.nev,interjuk_kapcs.projekt_id,interjuk_kapcs.jelolt_id,jeloltek.email,interjuk_kapcs.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interjuk_kapcs" +
                " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
                " WHERE jelolt_id = " + Session.ApplicantID+"" +
                " AND projekt_id="+ Session.ProjektID+"" +
                " ORDER BY felvitel_datum";
            List<ModelInterview> list = ModelInterview.GetModelInterview(command);
            MySql.Close();
            return list;
        }


        public void addInterview(string interju_datum, string cim, string leiras, string helyszin, string idopont) // javítva
        {
            DateTime dateTime = DateTime.Now;
            string command = "INSERT INTO `interjuk_kapcs` (`projekt_id`, `jelolt_id`, `hr_id`, `felvitel_datum`, `interju_datum`, `interju_cim`, `interju_leiras`, `helyszin`,  `idopont`) VALUES (" + Session.ProjektID + ", " + Session.ApplicantID + ", " + Session.UserData[0].id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "', '" + interju_datum + "', '" + cim + "', '" + leiras + "', '" + helyszin + "', '" + idopont + "');";
            MySql.Update(command);
            MySql.Close();
        }

        public void interviewDelete(int id) // javítva
        {
            MySql.Update("DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.id=" + id + " AND hr_id=" + Session.UserData[0].id + "");
            MySql.Update("DELETE FROM interju_resztvevo_kapcs WHERE interju_id=" + id + " ");
            MySql.Close();
        }



        public List<ModelKompetenciaSummary> Data_KompetenciaJeloltKapcs() // javítva
        {
            string command = "SELECT coalesce(AVG(k1_val),0) as k1_val,coalesce(AVG(k2_val),0) as k2_val,coalesce(AVG(k3_val),0) as k3_val,coalesce(AVG(k4_val),0) as k4_val,coalesce(AVG(k5_val),0) as k5_val, tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + Session.ApplicantID +" AND projekt_id = "+ Session.ProjektID+"";
            List < ModelKompetenciaSummary > list = ModelKompetenciaSummary.GetModelKompetenciaSummary(command);
            MySql.Close();
            return list;
        }
        
        public List<ModelTamogatas> Data_KompetenciaTamogatas() // javítva
        {
            string command = "SELECT tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + Session.ApplicantID + " AND projekt_id = " + Session.ProjektID + "";
            List<ModelTamogatas> list = ModelTamogatas.GetModelTamogatas(command);
            MySql.Close();
            return list;
        }







        
        //public void AddInterPlusOne()
        //{
        //    string command = "UPDATE projekt_jelolt_kapcs SET allapot = allapot + 1 WHERE projekt_id = "+pcontrol.ProjektID+ " AND jelolt_id = "+acontrol.ApplicantID+"";
        //    mysql.update(command);
        //}

        //public void progress_delete()
        //{
        //    string command = "UPDATE projekt_jelolt_kapcs SET allapot = allapot - 1 WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
        //    mysql.update(command);
        //}
    }
}
