﻿using System;
using System.Collections.Generic;
using HR_Portal.Source;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Project;

namespace HR_Portal.Control
{
    class ControlApplicantProject
    {
        ControlApplicant aControl = new ControlApplicant();
        ControlProject pControl = new ControlProject();
        Source.MySql mySql = new Source.MySql();

        public void telephoneFilterInsert(int ismerte,int muszakok,string utazas) //javított
        {
            string command = "UPDATE projekt_jelolt_kapcs SET allapota = 1 WHERE projekt_id = " + Session.ProjektID + " AND jelolt_id = " + Session.ApplicantID + "";
            Source.MySql.update(command);
            command = "UPDATE jeloltek SET pmk_ismerte = "+ismerte+"  WHERE id = " + Session.ApplicantID + "";
            Source.MySql.update(command);
            command = "INSERT INTO jelolt_statisztika (id, jelolt_id, utazas, muszakok) VALUES(null, "+ Session.ApplicantID+", '"+utazas+"', "+muszakok+")";
            Source.MySql.update(command);
            Source.MySql.close();
        }
        
        public List<ModelInterview> Data_Interview() //javított
        {
            string command = "SELECT interjuk_kapcs.id,megnevezes_projekt,jeloltek.nev,interjuk_kapcs.projekt_id,interjuk_kapcs.jelolt_id,jeloltek.email,interjuk_kapcs.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interjuk_kapcs" +
                " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
                " WHERE jelolt_id = " + Session.ApplicantID+"" +
                " AND projekt_id="+ Session.ProjektID+"" +
                " ORDER BY felvitel_datum";
            List<ModelInterview> list = ModelInterview.getModelInterview(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelInterview> Data_InterviewById() //javított
        {
            string command = "SELECT interjuk_kapcs.id,megnevezes_projekt,jeloltek.nev,interjuk_kapcs.projekt_id,interjuk_kapcs.jelolt_id,jeloltek.email,interjuk_kapcs.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interjuk_kapcs" +
                " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
                " WHERE interjuk_kapcs.id = " + Session.InterViewID + "" +
                " ORDER BY felvitel_datum";
            List<ModelInterview> list = ModelInterview.getModelInterview(command);
            Source.MySql.close();
            return list;
        }

        public void addInterview(string interju_datum, string cim, string leiras, string helyszin, string idopont) // javítva
        {
            DateTime dateTime = DateTime.Now;
            string command = "INSERT INTO `interjuk_kapcs` (`projekt_id`, `jelolt_id`, `hr_id`, `felvitel_datum`, `interju_datum`, `interju_cim`, `interju_leiras`, `helyszin`,  `idopont`) VALUES (" + Session.ProjektID + ", " + Session.ApplicantID + ", " + Session.UserData[0].id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "', '" + interju_datum + "', '" + cim + "', '" + leiras + "', '" + helyszin + "', '" + idopont + "');";
            Source.MySql.update(command);
            Source.MySql.close();
        }

        public void interviewDelete(int id) // javítva
        {
            Source.MySql.update("DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.id=" + id + " AND hr_id=" + Session.UserData[0].id + "");
            Source.MySql.update("DELETE FROM interju_resztvevo_kapcs WHERE interju_id=" + id + " ");
            Source.MySql.close();
        }

        public List<ModelKompetenciak> Data_Kompetencia() // javítva használja: newprojectpanel
        {
            List<ModelKompetenciak> list = ModelKompetenciak.getModelKompetenciak("SELECT * FROM kompetenciak");
            Source.MySql.close();
            return list;
        }

        public List<ModelKompetenciaSummary> Data_KompetenciaJeloltKapcs() // javítva
        {
            string command = "SELECT coalesce(AVG(k1_val),0) as k1_val,coalesce(AVG(k2_val),0) as k2_val,coalesce(AVG(k3_val),0) as k3_val,coalesce(AVG(k4_val),0) as k4_val,coalesce(AVG(k5_val),0) as k5_val, tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + Session.ApplicantID +" AND projekt_id = "+ Session.ProjektID+"";
            List < ModelKompetenciaSummary > list = ModelKompetenciaSummary.getModelKompetenciaSummary(command);
            Source.MySql.close();
            return list;
        }
        
        public List<ModelTamogatas> Data_KompetenciaTamogatas() // javítva
        {
            string command = "SELECT tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + Session.ApplicantID + " AND projekt_id = " + Session.ProjektID + "";
            List<ModelTamogatas> list = ModelTamogatas.getModelTamogatas(command);
            Source.MySql.close();
            return list;
        }

        public void kompetenciaUpdate(List<int> list) // javítva használja: interviewpanel
        {
            string command = "INSERT INTO `kompetencia_jelolt_kapcs` (`id`, `interju_id`, `projekt_id`, `jelolt_id`, `hr_id`, `k1_id`, `k1_val`, `k2_id`, `k2_val`, `k3_id`, `k3_val`, `k4_id`, `k4_val`, `k5_id`, `k5_val`, tamogatom) VALUES (null, " + Session.InterViewID + ", " + Session.ProjektID + ", " + Session.ApplicantID + ", " + Session.UserData[0].id + ", "+list[0]+ ", " + list[1] + ", " + list[2] + ", " + list[3] + ", " + list[4] + ", " + list[5] + ", " + list[6] + ", " + list[7] + ", " + list[8] + ", " + list[9] + ", " + list[10] + ");";
            Source.MySql.update(command);
            Source.MySql.close();
        }

        public bool hasKompetencia() // javítva
        {
            string command = "SELECT * FROM kompetencia_jelolt_kapcs WHERE interju_id = "+ Session.InterViewID + " AND hr_id = "+ Session.UserData[0].id+"";
            bool response = mySql.bind(command);
            Source.MySql.close();
            return response;
        }

        public List<ModelErtesitendok> Data_ProjektErtesitendokKapcsolt() // javítva használja: interviewpanel
        {
            string command = "SELECT users.id, name, email FROM users INNER JOIN projekt_ertesitendok_kapcs ON ertesitendok_id = users.id WHERE projekt_id = "+ Session.ProjektID+"";
            List<ModelErtesitendok> list = ModelErtesitendok.getModelErtesitendok(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelErtesitendok> Data_InterjuErtesitendokKapcsolt() // javítva használja: interviewpanel
        {
            string command = "SELECT users.id, name, email FROM interju_resztvevo_kapcs left JOIN users ON user_id = users.id WHERE interju_id = " + Session.InterViewID + " GROUP BY users.id";
            List<ModelErtesitendok> list = ModelErtesitendok.getModelErtesitendok(command);
            Source.MySql.close();
            return list;
        }

        public void insertInterviewInvited(int id) // javítva
        {
            string command = "INSERT INTO `interju_resztvevo_kapcs` (`id`, `interju_id`, `user_id`) VALUES (NULL, "+ Session.InterViewID +", "+id+");";
            Source.MySql.update(command);
            Source.MySql.close();
        }

        public void deleteInterviewInvited(int id) // javítva
        {
            string command = "DELETE FROM `interju_resztvevo_kapcs` WHERE user_id = " + id + "";
            Source.MySql.update(command);
            Source.MySql.close();
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
